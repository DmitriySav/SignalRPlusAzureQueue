using System.Data.Entity;
using MessageConsumer.Domain.Core;

namespace MessageConsumer.Infrastructure.Data
{
    public class UserContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public UserContext():base("name=UserContext")
        {           
           
            
        }
    }
}