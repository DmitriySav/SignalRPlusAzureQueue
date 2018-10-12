using System;
using System.Collections.Generic;
using System.Data.Entity;
using MessageConsumer.Domain.Core;
using MessageConsumer.Domain.Interfaces;
using MessageConsumer.Infrastructure.Data;

namespace MessageConsumer.Repositories
{
    public class UserRepository:IRepository<User>
    {
        private UserContext db;

        public UserRepository(UserContext userContext)
        {
            db = userContext;
        }


        public IEnumerable<User> List()
        {
            return db.Users.Include(r => r.Roles);
        }

        public User Create(User item)
        {
            db.Users.Add(item);
            return item;
        }

        public void Delete(Guid id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }            
        }

        public User Get(Guid id)
        {
            return db.Users.Find(id);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}