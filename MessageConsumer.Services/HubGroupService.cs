using System.Collections.Generic;
using System.Linq;
using MessageConsumer.Entities;
using MessageConsumer.Services.Interfaces;

namespace MessageConsumer.Services
{
    

    /// <summary>
    /// Class for managing user in HUb
    /// </summary>
    /// <inheritdoc cref="IHubGroupManager{T}"/>
    public class HubGroupManager<T>:IHubGroupManager<T>
    {
        public Dictionary<T, List<HubUser>> RolesGroup = new Dictionary<T, List<HubUser>>();
        private readonly object _locker = new object();

        /// <inheritdoc />
        /// <summary>
        /// Add user to group
        /// </summary>
        /// <param name="key">Group name</param>
        /// <param name="userId">User identifier</param>
        /// <param name="connectionId">User connection identifier</param>
        public void AddToGroup(T key, string userId, string connectionId)
        {
            lock (_locker)
            {

                if (!RolesGroup.ContainsKey(key))
                    RolesGroup.Add(key, new List<HubUser> { new HubUser(userId, connectionId) });
                
                else if (RolesGroup.ContainsKey(key))
                {
                    var user = RolesGroup[key].Find(x => x.UserId == userId);

                    if (user == null)
                        RolesGroup[key].Add(new HubUser(userId, connectionId));   
                    
                    user?.ConnectionIds.Add(connectionId);
                }               
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Get groups of user
        /// </summary>
        /// <param name="connectionId">User connection identifier</param>
        /// <returns>Enumerable of roles</returns>
        public IEnumerable<T> GetUserGroupByConnectionId(string connectionId)
        {
            lock (RolesGroup)
            {
                foreach (var role in RolesGroup)
                {
                    var user = role.Value
                        .Find(x => x.ConnectionIds.Contains(connectionId));

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
                    var user = role.Value.Find(x => x.UserId == userId);
                    if (user != null) yield return role.Key;
                }
            }
        }

        /// <summary>
        /// Get users from group
        /// </summary>
        /// <param name="group">Name of group</param>
        /// <returns>Enumerable of users in group</returns>
        public IEnumerable<HubUser> GetUsersFromGroup(T group)
        {
            lock (_locker)
            {
                if (RolesGroup.ContainsKey(group))
                    return RolesGroup[group];              
            }

            return Enumerable.Empty<HubUser>();
        }


        /// <summary> 
        /// Get all connection identifier in group
        /// </summary>
        /// <param name="group">name of group</param>
        /// <returns>Enumerable of connection ids string type </returns>
        public IEnumerable<string> GetConnectionIdFromGroup(T group)
        {
            lock (_locker)
            {
                var connectionIdList = new List<string>();
                if (RolesGroup.ContainsKey(group))
                {
                    foreach (var user in RolesGroup[group])
                    {
                       connectionIdList.AddRange(user.ConnectionIds);
                    }
                }
                return connectionIdList;
            }
        }

        /// <summary>
        /// Remove user connection from group
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="connectionId">user connection identifier</param>
        public void RemoveFromGroup(string userId, string connectionId)
        {
            lock (_locker)
            {
                //var tempDict = new Dictionary<T, List<HubUser>>(RolesGroup);

                foreach (var item in RolesGroup.ToList())
                {
                    var test = RolesGroup[item.Key];
                    var user = test.Find(x => x.UserId == userId);
                    //var user = test.FirstOrDefault(x => x.UserId == userId);

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