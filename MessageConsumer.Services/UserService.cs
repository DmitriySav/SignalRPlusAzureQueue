using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MessageConsumer.Domain.Core;
using MessageConsumer.Domain.Interfaces;
using MessageConsumer.Infrastructure.Data.DtoModels;
using MessageConsumer.Services.Interfaces;
using MessageConsumer.Utils.Interfaces;

namespace MessageConsumer.Services
{
    /// <summary>
    /// Service for user management
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IPasswordUtil _passwordUtil;

        public UserService(IRepository<User> repo, IPasswordUtil passUtil)
        {
            _userRepo = repo;
            _passwordUtil = passUtil;
        }

        /// <summary>
        /// Method check user's credentials
        /// </summary>
        /// <param name="userEmail"> User email string</param>
        /// <param name="password"> User's password</param>
        /// <returns>bool value, true if user is authenticated</returns>
        public bool HasAuthenticate(string userEmail, string password)
        {
            if (IsValidEmail(userEmail))
            {

                var user = GetByEmail(userEmail);
                if (user == null)
                {
                    return false;
                }
                bool result = _passwordUtil.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
                return result;
            }

            return false;
        }


        private User GetByEmail(string email)
        {
            var user = _userRepo
                .List()
                .SingleOrDefault(x => String.Equals(x.UserEmail, email, StringComparison.OrdinalIgnoreCase));
            return user;
        }

        /// <summary>
        /// Get user DTO model by email
        /// </summary>
        /// <param name="email">string user email</param>
        /// <returns>user DTO model</returns>
        public UserDTO GetUserByEmail(string email)
        {
            if (!IsValidEmail(email))
            {
                throw new Exception("Email is invalid");
            }

            var user = GetByEmail(email);
            if (user == null) return null;
            var userDto = new UserDTO
            {
                UserId = user.UserGuid,
                UserEmail = user.UserEmail,
                Roles = user.Roles,
            };
            return userDto;

        }

        /// <summary>
        /// Get user DTO model by id
        /// </summary>
        /// <param name="id">User Guid</param>
        /// <returns>return UserDto model without password</returns>
        public UserDTO GetUserById(Guid id)
        {
            var user = _userRepo.Get(id);
            var userDto = new UserDTO
            {
                UserId = user.UserGuid,
                UserEmail = user.UserEmail,
                Roles = user.Roles,
            };
            return userDto;
        }

        /// <summary>
        /// Create new user with hashing password
        /// </summary>
        /// <param name="userDto">user DTO model</param>
        public void RegisterNewUser(UserDTO userDto)
        {
            if (!IsValidEmail(userDto.UserEmail))
            {
                throw new Exception("Invalid Email address");
            }

            if (userDto.Password.Length < 6)
            {
                throw new Exception("Password is short");
            }

            var uer = GetUserByEmail(userDto.UserEmail);
            if (uer != null)
            {
                return;
            }
            var (passwordHash, passwordSalt) = _passwordUtil.CreatePasswordHash(userDto.Password);
            User user = new User()
            {
                UserGuid = Guid.NewGuid(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserEmail = userDto.UserEmail.ToLower(),
                Roles = userDto.Roles
            };
            _userRepo.Create(user);
        }

        /// <summary>
        /// Save changes in database
        /// </summary>
        public void SaveChanges()
        {
            _userRepo.SaveChanges();
        }


        /// <summary>
        /// Check if email valid
        /// </summary>
        /// <param name="source">pass user email</param>
        /// <returns>bool value</returns>
        public bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
    }
}