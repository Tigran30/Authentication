using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Base
{
    public class RegisterationModel
    {
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Password { set; get; }
        public string RepeatPassword { set; get; }
    }
    
}
