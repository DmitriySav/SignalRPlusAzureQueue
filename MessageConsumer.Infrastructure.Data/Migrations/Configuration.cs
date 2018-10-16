using System.Collections.Generic;
using MessageConsumer.Domain.Core;
using MessageConsumer.Infrastructure.Data.DtoModels;
using MessageConsumer.Services.Interfaces;
using MessageConsumer.Util;

namespace MessageConsumer.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MessageConsumer.Infrastructure.Data.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MessageConsumer.Infrastructure.Data.UserContext";
        }

        protected override void Seed(MessageConsumer.Infrastructure.Data.UserContext context)
        {
            var userRole = new Role { RoleId = 0, RoleName = "User" };
            var adminRole = new Role { RoleId = 1, RoleName = "Admin" };
            var umpireRole = new Role { RoleId = 2, RoleName = "Umpire" };
            var coachRole = new Role { RoleId = 3, RoleName = "Coach" };

            var passwordUtil = new PasswordUtil();

            var password = passwordUtil.CreatePasswordHash("adminpassword");
            context.Users.Add(new User
            {
                UserGuid = Guid.NewGuid(),
                UserEmail = "admin@gmail.com",
                PasswordHash = password.passwordHash,
                PasswordSalt = password.passwordSalt,
                Roles = new List<Role> {adminRole, umpireRole }

            });
            password = passwordUtil.CreatePasswordHash("password");
            context.Users.Add(new User
            {
                UserGuid = Guid.NewGuid(),
                UserEmail = "user@gmail.com",
                PasswordHash = password.passwordHash,
                PasswordSalt = password.passwordSalt,
                Roles = new List<Role> { userRole }
            });
            password = passwordUtil.CreatePasswordHash("passlord");
            context.Users.Add(new User
            {
                UserGuid = Guid.NewGuid(),
                UserEmail = "coach@gmail.com",
                PasswordHash = password.passwordHash,
                PasswordSalt = password.passwordSalt,
                Roles = new List<Role> { userRole, coachRole }
            });
            context.SaveChanges();
        }
    }
}
