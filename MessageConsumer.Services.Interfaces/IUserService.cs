using System;
using MessageConsumer.Infrastructure.Data.DtoModels;

namespace MessageConsumer.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Method check user's credentials
        /// </summary>
        /// <param name="userName"> User name string</param>
        /// <param name="password"> User's password</param>
        /// <returns>bool value, true if user is authenticated</returns>
        bool HasAuthenticate(string userEmail, string password);

        UserDTO GetUserById(Guid id);
        UserDTO GetUserByEmail(string userEmail);
        void RegisterNewUser(UserDTO userDto);
        void SaveChanges();

    }
}
