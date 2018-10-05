using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Models;
using SignalRPlusAzureQueue.Sevices;

namespace SignalRPlusAzureQueue.Util
{
    public static class UserInitializer
    {
        public static void Init(UserService userService)
        {
            var userRole = new Role{RoleId = 0, RoleName = "User"};
            var adminRole = new Role {RoleId = 1, RoleName = "Admin"};
            var umpireRole = new Role {RoleId = 2, RoleName = "Umpire"};
            var coachRole = new Role {RoleId = 3, RoleName = "Coach"};

            userService.Create(new UserDTO
            {
                UserEmail = "admin@gmail.com",
                Password = "password",
                Roles = new List<Role>{umpireRole, adminRole}

            });
            userService.Create(new UserDTO
            {
                UserEmail = "user@gmail.com",
                Password = "password",
                Roles = new List<Role> {userRole, coachRole}

            });
            userService.Create(new UserDTO
            {
                UserEmail = "coach@gmail.com",
                Password = "passlord",
                Roles = new List<Role> { coachRole}

            });
        }

            
        }
    }
