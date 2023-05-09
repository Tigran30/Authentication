using System;
using System.Collections.Generic;

namespace Authentication.Database.Entities
{
    public partial class ApplicaitonUser
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
