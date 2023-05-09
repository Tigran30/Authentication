using System;
using System.Collections.Generic;

namespace Authentication.Database.Entities
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int UserId { get; set; }
        public string Token { get; set; } = null!;
        public bool IsUsed { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
