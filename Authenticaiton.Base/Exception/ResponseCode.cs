using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Base
{
    public enum ResponseCode : int
    {
        AlreadyRegistered = 1,
        EmailAlreadyExists = 2,
        InvalidEmailAddress = 3,
        PasswordsDontMatch = 4,
        InvalidPhoneNumber = 5,
        IncorrectPassword = 6,
        InvalidRefreshToken = 7,
        InvalidEmailOrPassword = 8,
        UserNotFound = 9,
        NewPhoneNumberMatchesOld = 10,
        NewEmailMatchesOld =11,
        InternalServerError = 12

    }
}
