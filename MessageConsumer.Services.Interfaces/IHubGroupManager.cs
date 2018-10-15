using System.Collections.Generic;
using MessageConsumer.Entities;

namespace MessageConsumer.Services.Interfaces
{
    public interface IHubGroupManager<T>
    {

        /// <summary>
        /// Add user to group
        /// </summary>
        /// <param name="key">Group name</param>
        /// <param name="userId">User identifier</param>
        /// <param name="connectionId">User connection identifier</param>
        void AddToGroup(T key, string userId, string connectionId);

        /// <summary>
        /// Get groups of user
        /// </summary>
        /// <param name="connectionId">User connection identifier</param>
        /// <returns>Enumerable of roles</returns>
        IEnumerable<T> GetUserGroupsByConnectionId(string connectionId);


        /// <summary>
        /// Get all connection identifier in group
        /// </summary>
        /// <param name="group">name of group</param>
        /// <returns>Enumerable of connection ids string type </returns>
        IEnumerable<string> GetConnectionIdFromGroup(T group);
        /// <summary>
        /// Get groups of user
        /// </summary>
        /// <param name="userId">User identifier </param>
        /// <returns>Enumerable of roles</returns>
        IEnumerable<T> GetUserGroupsById(string userId);

        /// <summary>
        /// Get users from group
        /// </summary>
        /// <param name="role"> Name of role</param>
        /// <returns>Enumerable of users in group</returns>
        IEnumerable<HubUser> GetUsersFromGroup(T role);

        /// <summary>
        /// Remove user connection from group
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="connectionId">user connection identifier</param>
        void RemoveFromGroup(string userId, string connectionId);
    }
}