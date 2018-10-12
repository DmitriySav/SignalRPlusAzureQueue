using System.Collections.ObjectModel;


namespace MessageConsumer.Domain.Core
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Collection<User> Users { get; set; }
    }
}