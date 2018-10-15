using System;
using System.Collections.Generic;
using MessageConsumer.Domain.Interfaces;
using MessageConsumer.Services;
using MessageConsumer.Infrastructure.Data.DtoModels;
using MessageConsumer.Util;
using MessageConsumer.Utils.Interfaces;
using Moq;
using NUnit.Framework;
using User = MessageConsumer.Domain.Core.User;

namespace MessageConsumer.Tests
{
    /// <summary>
    /// Summary description for UserServiceTests
    /// </summary>
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IRepository<User>> repo;
        public UserServiceTests()
        {
            repo = new Mock<IRepository<User>>();

        }        
        
        [Test]
        public void Create_WithInvalidEmail_Exeption()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);


            Assert.Throws<Exception>(() =>
            {
                userService.RegisterNewUser(new UserDTO
                {
                    UserEmail = "testemaiil.com",
                    Password = "Password2"

                });
            });
        }

        [Test]
        public void IsValidEmail_PassInvalidEmail_returnFalse()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            var valid = userService.IsValidEmail("fewj.@wqed.com");

            Assert.False(valid);
        }

        [Test]
        public void IsValidEmail_PassValidEmail_returnTrue()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            var valid = userService.IsValidEmail("NamE@gmail.com");

            Assert.True(valid);
        }

        [Test]
        public void HasAuthenticate_whenCalledWithCorrectParameters_ReturnTrue()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            var (passwordHash, passwordSalt) = passwordUtil.CreatePasswordHash("password");
            repo.Setup(g => g.List()).Returns(new List<User>
            {
                new User
                {
                    UserEmail = "name@gmail.com",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash

                }
            });

            var result = userService.HasAuthenticate("NamE@gmail.com", "password");
            Assert.True(result);

        }

        [Test]
        public void HasAuthenticate_whenCalledWithLetterFromOtherCulture_ReturnFalse()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            var (passwordHash, passwordSalt) = passwordUtil.CreatePasswordHash("Password2");
            repo.Setup(g => g.List()).Returns(new List<User>
            {
                new User
                {
                    UserEmail = "testemail@mail.com",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash

                }
            });
            
            var result = userService.HasAuthenticate("testemail@mail.com", "Passwоrd2");

            Assert.False(result);
        }
        [Test]

        public void HasAuthenticate_whenCalledWithInvalidEmail_ReturnFalse()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            var (passwordHash, passwordSalt) = passwordUtil.CreatePasswordHash("Password2");
            repo.Setup(g => g.List()).Returns(new List<User>
            {
                new User
                {
                    UserEmail = "testem*@mail.com",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash

                }
            });
            
            var result = userService.HasAuthenticate("testemail@mail.com", "Password2");

            Assert.False(result);
        }

        [Test]
        public void HasAuthenticate_whenCalledWithValidEmail_ReturnTrue()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            var (passwordHash, passwordSalt) = passwordUtil.CreatePasswordHash("Password2");
            repo.Setup(g => g.List()).Returns(new List<User>
            {
                new User
                {
                    UserEmail = "testemail@mail.com",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash

                }
            });
           var result = userService.HasAuthenticate("testemail@mail.com", "Password2");

            Assert.True(result);
        }

        [Test]
        public void RegisterNewUser_whenCalledWithShortPassword_Exception()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            repo.Setup(c => c.List()).Returns(new List<User>());

            Assert.Throws<Exception>(() =>
            {
                userService.RegisterNewUser(new UserDTO
                {
                    UserId = Guid.NewGuid(),
                    UserEmail = "admin@gmail.com",
                    Password = "pass"
                });
            });
        }

        [Test]
        public void RegisterNewUser_WhenCalled_verifyRepoMethodCreate()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            userService.RegisterNewUser(new UserDTO
            {
                UserId = Guid.NewGuid(),
                UserEmail = "admin@gmail.com",
                Password = "pass23414"
            });


            repo.Verify(c => c.Create(It.IsAny<User>()));
        }

        [Test]
        public void GetUserByEmail_WhenCalledWithNonExistingUser_returnNull()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            var (passwordHash, passwordSalt) = passwordUtil.CreatePasswordHash("Password2");
            repo.Setup(g => g.List()).Returns(new List<User>
            {
                new User
                {
                    UserEmail = "testemail@mail.com",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash

                }
            });

            var user = userService.GetUserByEmail("nonExisting@mail.ru");
            Assert.IsNull(user);
        }

        [Test]
        public void GetUserByEmail_WhenCalledWithExistingUser_IsNotNull()
        {
            IPasswordUtil passwordUtil = new PasswordUtil();
            var userService = new UserService(repo.Object, passwordUtil);
            var (passwordHash, passwordSalt) = passwordUtil.CreatePasswordHash("Password2");
            repo.Setup(g => g.List()).Returns(new List<User>
            {
                new User
                {
                    UserEmail = "testemail@mail.com",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash

                }
            });

            var user = userService.GetUserByEmail("TesTemAil@mail.com");
            Assert.IsNotNull(user);
            Assert.IsInstanceOf<UserDTO>(user);
        }

    }
}
