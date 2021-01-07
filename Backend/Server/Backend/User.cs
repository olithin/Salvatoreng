using System.Collections.Generic;

namespace Server.Backend
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
    }
}