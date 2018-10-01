using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Models;

namespace SignalRPlusAzureQueue.Sevices
{
    /// <summary>
    /// Service for user management
    /// </summary>
    public class UserService : IUserService
    {
        public static List<UserModel> Users;

        public UserService()
        {
            Users = new List<UserModel>();
            Create(new UserDTO
            {
                UserEmail = "name@gmail.com",
                Password = "password"
            });
        }

        /// <summary>
        /// Method check user's credentials
        /// </summary>
        /// <param name="userEmail"> User email string</param>
        /// <param name="password"> User's password</param>
        /// <returns>bool value, true if user is authenticated</returns>
        public bool IsAuthenticate(string userEmail, string password)
        {
            if (IsValidEmail(userEmail))
            {
                var user = Users.SingleOrDefault(x => x.UserEmail == userEmail);
                if (user == null)
                {
                    return false;
                }
                bool result = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
                return result;
            }

            return false;
        }

        /// <summary>
        /// Create new user with hashing password
        /// </summary>
        /// <param name="userDto">user DTO model</param>
        public void Create(UserDTO userDto)
        {
            if (!IsValidEmail(userDto.UserEmail))
            {
                throw new Exception("Invalid Email address");
            }

            byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);
                UserModel user = new UserModel()
                {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    UserEmail = userDto.UserEmail
                };
                Users.Add(user);
            
        }

        
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
           
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
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