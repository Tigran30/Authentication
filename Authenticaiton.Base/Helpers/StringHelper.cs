using System.Text;
using System.Text.RegularExpressions;

namespace Authentication.Base.Helpers
{
    public static class StringHelper
    {
        public static Dictionary<ResponseCode, string> GetMessages()
        {
            var messages = new Dictionary<ResponseCode, string>();
            messages.Add(ResponseCode.AlreadyRegistered, "User already registered");
            messages.Add(ResponseCode.EmailAlreadyExists, "Email is in use write another email");
            messages.Add(ResponseCode.InvalidEmailAddress, "Invalid Email Address");
            messages.Add(ResponseCode.PasswordsDontMatch, "Password Dont Match");
            messages.Add(ResponseCode.InvalidPhoneNumber, "Invalid PhoneNumber");
            messages.Add(ResponseCode.IncorrectPassword, "Incorrect Password");
            messages.Add(ResponseCode.InvalidRefreshToken, "Invalid RefreshToken");
            messages.Add(ResponseCode.InvalidEmailOrPassword, "Invalid Email Or Password");
            messages.Add(ResponseCode.UserNotFound, "User Not Found");
            messages.Add(ResponseCode.NewPhoneNumberMatchesOld, "New PhoneNumber Matches Old");
            messages.Add(ResponseCode.NewEmailMatchesOld, "NewEmail Matches Old");
            messages.Add(ResponseCode.InternalServerError, "Internal error");
            return messages;
        }

        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }      

        public static string GenerateHash(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            var hash = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static bool ValidatePassword(string password, string salt, string hash)
        {
            if(GenerateHash(password, salt) != hash)
            {
                return false;
            }
            else
                return true;
        }

        public static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, "\\+?[1-9][0-9]{7,14}");
        }

    }
}
