using System.Collections.Generic;

namespace Server.Backend
{
    public class Role
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}