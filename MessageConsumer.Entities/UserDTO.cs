using System;
using System.Collections.Generic;
using MessageConsumer.Domain.Core;

namespace MessageConsumer.Infrastructure.Data.DtoModels
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public virtual List<Role> Roles { get; set; }

    }
}