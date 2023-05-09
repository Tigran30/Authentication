using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Base.Models
{
    public class LoginResponseModel
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
