using Authentication.Base.Models;

namespace Authentication.Base
{
    public interface IAuthenticationService
    {
        Task<LoginResponseModel> Login(AuthenticateModel model);
        Task<LoginResponseModel> RefreshToken(string refreshToken);
        Task<bool?> ChangeEmail(string email, int? userId);
        Task<bool?> ChangeFirstName(string firstName, int? userId);
        Task<bool?> ChangeLastName(string LastName, int? userId);
        Task<bool?> ChangePhoneNumber(string newPhoneNumber, int? userId);
    }
}
