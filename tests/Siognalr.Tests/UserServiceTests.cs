using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using SignalRPlusAzureQueue.Models;
using SignalRPlusAzureQueue.Sevices;

namespace Siognalr.Tests
{
    /// <summary>
    /// Summary description for UserServiceTests
    /// </summary>
    //[TestFixture]
//    public class UserServiceTests
//    {
//        public UserServiceTests()
//        {
//            //
//            // TODO: Add constructor logic here
//            //
//        }

//        [Test]
//        public void UserService_WhenCreateObject_OneUserExist()
//        {
//            var userService = new UserService();

//            var usersCount = UserService.Users.Count;

//            Assert.AreEqual(1, usersCount);
//        }

//        [Test]
//        public void Create_WithCorrectEmail_returnTwoUsers()
//        {
//            var userService = new UserService();
//            userService.Create(new UserDTO
//            {
//                UserEmail = "testemail@mail.com",
//                Password = "Password2"

//            });
//            var userCount = UserService.Users.Count;

//            Assert.AreEqual(2, userCount);
//        }

//        [Test]
//        public void Create_WithInvalidEmail_Exeption()
//        {
//            var userService = new UserService();


//            Assert.Throws<Exception>(() =>
//            {
//                userService.Create(new UserDTO
//                {
//                    UserEmail = "testemaiil.com",
//                    Password = "Password2"

//                });
//            });
//        }

//        [Test]
//        public void IsValidEmail_PassInvalidEmail_returnFalse()
//        {
//            var userService = new UserService();
//            var valid = userService.IsValidEmail("fewj.@wqed.com");

//            Assert.False(valid);
//        }

//        [Test]
//        public void IsValidEmail_PassValidEmail_returnTrue()
//        {
//            var userService = new UserService();
//            var valid = userService.IsValidEmail("NamE@gmail.com");

//            Assert.True(valid);
//        }

//        [Test]
//        public void IsAuthenticate_whenCalledWithCorrectParameters_ReturnTrue()
//        {
//            var userService = new UserService();
            
//            var result = userService.HasAuthenticate("NamE@gmail.com", "password");
//            Assert.True(result);

//        }

//        [Test]
//        public void IsAuthenticate_whenCalledWithLetterFromOtherCulture_ReturnFalse()
//        {
//            var userService = new UserService();
//            userService.Create(new UserDTO
//            {
//                UserEmail = "testemail@mail.com",
//                Password = "Password2"

//            });
//            var result = userService.HasAuthenticate("testemail@mail.com", "Passwоrd2");

//            Assert.False(result);
//        }
//        [Test]

//        public void IsAuthenticate_whenCalledWithInvalidEmail_ReturnFalse()
//        {
//            var userService = new UserService();
//            userService.Create(new UserDTO
//            {
//                UserEmail = "testemail@mail.com",
//                Password = "Password2"

//            });
//            var result = userService.HasAuthenticate("testem*@mail.com", "Password2");

//            Assert.False(result);
//        }

//    }
}
