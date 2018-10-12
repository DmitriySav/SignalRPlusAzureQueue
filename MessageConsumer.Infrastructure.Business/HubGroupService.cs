using System;
using System.Collections.Generic;
using System.Linq;
using MessageConsumer.Services.Interfaces;
using MessageConsumer.Entities;

namespace MessageConsumer.Infrastructure.Business
{
    

    /// <summary>
    /// Class for managing user in HUb
    /// </summary>
    public class HubGroupManager<T>:IHubGroupManager<T>
    {
        public Dictionary<T, List<HubUser>> RolesGroup = new Dictionary<T, List<HubUser>>();
        public object obj = new Object();

        /// <summary>
        /// Add user to group
        /// </summary>
        /// <param name="key">Group name</param>
        /// <param name="userId">User identifier</param>
        /// <param name="connectionId">User connection identifier</param>
        public void AddToGroup(T key, string userId, string connectionId)
        {
            lock (RolesGroup)
            {

                if (!RolesGroup.ContainsKey(key))
                {
                    RolesGroup.Add(key, new List<HubUser> { new HubUser(userId, connectionId) });
                }

                else if (RolesGroup.ContainsKey(key))
                {
                    var user = RolesGroup[key].FirstOrDefault(x => x.UserId == userId);
                    if (user == null)
                    {
                        RolesGroup[key].Add(new HubUser(userId, connectionId));
                    }
                    else
                    {
                        lock (user)
                        {
                            user.ConnectionIds.Add(connectionId);
                        }
                    }

                }               
            }
        }
        /// <summary>
        /// Get groups of user
        /// </summary>
        /// <param name="connectionId">User connection identifier</param>
        /// <returns>Enumerable of roles</returns>
        public IEnumerable<T> GetUserGroupsByConnectionId(string connectionId)
        {
            lock (RolesGroup)
            {
                foreach (var role in RolesGroup)
                {
                    var user = role.Value
                        .SingleOrDefault(x => x.ConnectionIds.Contains(connectionId));

                    if (user != null) yield return role.Key;
                }
            }
        }

        /// <summary>
        /// Get groups of user
        /// </summary>
        /// <param name="userId">User identifier </param>
        /// <returns>Enumerable of roles</returns>
        public IEnumerable<T> GetUserGroupsById(string userId)
        {
            lock (RolesGroup)
            {
                foreach (var role in RolesGroup)
                {
                    var user = role.Value.SingleOrDefault(x => x.UserId == userId);
                    if (user != null) yield return role.Key;
                }
            }
        }
        /// <summary>
        /// Get users from group
        /// </summary>
        /// <param name="role"> Name of role</param>
        /// <returns>Enumerable of users in group</returns>
        public IEnumerable<HubUser> GetUsersFromGroup(T role)
        {
            lock (RolesGroup)
            {
                if (RolesGroup.ContainsKey(role))
                {
                    return RolesGroup[role];
                }
            }

            return Enumerable.Empty<HubUser>();
        }

        /// <summary>
        /// Remove user connection from group
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="connectionId">user connection identifier</param>
        public void RemoveFromGroup(string userId, string connectionId)
        {
            lock (obj)
            {
                //var tempDict = new Dictionary<T, List<HubUser>>(RolesGroup);

                foreach (var item in RolesGroup)
                {
                    var test = RolesGroup[item.Key];
                    var user = test.SingleOrDefault(x => x.UserId == userId);

                    if (user != null)
                        if (user.ConnectionIds.Count > 1)
                            user.ConnectionIds.Remove(connectionId);
                        else
                            RolesGroup[item.Key].Remove(user); 

                    if (RolesGroup[item.Key].Count == 0)
                        RolesGroup.Remove(item.Key);
                }
                //RolesGroup = RolesGroup;
            }
        }
    }
}