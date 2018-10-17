using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MessageConsumer.Entities;
using MessageConsumer.Entities.Enums;
using MessageConsumer.Entities.Events;
using MessageConsumer.Services.Interfaces;

namespace MessageConsumer.Services
{


    /// <summary>
    /// Class for managing user in HUb
    /// </summary>
    /// <inheritdoc cref="IHubGroupManager{T}"/>
    public class HubGroupManager<T> : IHubGroupManager<T>
    {
        public Dictionary<T, List<HubUser>> RolesGroup = new Dictionary<T, List<HubUser>>();
        private readonly object _locker = new object();



        public event EventHandler<ManagerEventArgs<T, HubUser>> OnGroupChange;


        /// <inheritdoc />
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

                OnGroupChange?.Invoke(this, new ManagerEventArgs<T, HubUser>
                {
                    Action = ManagerAction.AddItem,
                    Key = key,
                    Value = new HubUser(userId, connectionId)
                });
            }
        }

        /// <inheritdoc />
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
        /// Check if group is exist
        /// </summary>
        /// <param name="group"></param>
        /// <returns>bool value, true if group exist</returns>
        public bool IsGroupExist(T group)
        {
            lock (_locker)
            {
                return RolesGroup.ContainsKey(@group);
            }
        }

        /// <summary>
        /// Get all hubs user which register in hubManager
        /// </summary>
        /// <returns>Enumerable of users</returns>
        public IEnumerable<HubUser> GetAllUsers()
        {
            lock (_locker)
            {
                var users = new List<HubUser>();
                foreach (var group in RolesGroup.Values)
                {
                    users.AddRange(group);
                }

                return users;
            }
        }


        /// <inheritdoc/>
        public IEnumerable<T> GetAllGroups()
        {
            lock (_locker)
            {
                return RolesGroup.Keys;
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
                foreach (var item in RolesGroup.ToList())
                {
                    var user = RolesGroup[item.Key].Find(x => x.UserId == userId);

                    if (user != null)
                    {
                        if (user.ConnectionIds.Count > 1)
                            user.ConnectionIds.Remove(connectionId);
                        else
                            RolesGroup[item.Key].Remove(user);

                        OnGroupChange?.Invoke(this, new ManagerEventArgs<T, HubUser>
                        {
                            Action = ManagerAction.RemoveItem,
                            Key = item.Key,
                            Value = user
                        });
                    }


                    if (RolesGroup[item.Key].Count == 0)
                        RolesGroup.Remove(item.Key);
                }
            }
        }

        public void RemoveFromGroup(string userId)
        {
            lock (_locker)
            {
                foreach (var item in RolesGroup.ToList())
                {
                    var user = RolesGroup[item.Key].Find(x => x.UserId == userId);

                    if (user != null)
                    {
                        RolesGroup[item.Key].Remove(user);
                        OnGroupChange?.Invoke(this, new ManagerEventArgs<T, HubUser>
                        {
                            Action = ManagerAction.RemoveItem,
                            Key = item.Key,
                            Value = user
                        });
                    }

                    if (RolesGroup[item.Key].Count == 0)
                        RolesGroup.Remove(item.Key);
                }
            }

        }
    }
}