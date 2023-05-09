using Authentication.Base;
using Authentication.Base.Helpers;
using Authentication.Base.Models;
using Authentication.Database.Entities;

namespace Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public AuthenticationService(IApplicationUserRepository applicationUserRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResponseModel> Login(AuthenticateModel model)
        {
            ApplicaitonUser? user;
            if (StringHelper.IsEmailValid(model.Username))
            {
                user = applicationUserRepository.FirstOrDefault(x => x.Email == model.Username);
            }
            else
            {
                if (!StringHelper.IsPhoneNumberValid(model.Username))
                {
                    throw new AuthenticationException(ResponseCode.InvalidPhoneNumber);
                }

                user = applicationUserRepository.FirstOrDefault(x => x.PhoneNumber == model.Username);
            }

            if (user == null)
            {
                throw new AuthenticationException(ResponseCode.InvalidEmailOrPassword);
            }

            var validated = StringHelper.ValidatePassword(model.Password, user.PasswordSalt, user.PasswordHash);
            if (!validated)
            {
                throw new AuthenticationException(ResponseCode.IncorrectPassword);
            }

            var refreshToken = new RefreshToken
            {
                ExpirationDate = DateTime.Now.AddDays(1),
                IsUsed = false,
                PhoneNumber = user.PhoneNumber,
                Token = StringHelper.GenerateRandomString(25) + Guid.NewGuid().ToString(),
            };

            await refreshTokenRepository.AddAsync(refreshToken);
            await refreshTokenRepository.SaveAsync();

            return new LoginResponseModel
            {
                UserId = user.Id,
                RefreshToken = refreshToken.Token,
            };
        }

        public async Task<LoginResponseModel> RefreshToken(string token)
        {
            var refreshToken = refreshTokenRepository.FirstOrDefault(x => x.Token == token);
            if (refreshToken == null)
            {
                throw new AuthenticationException(ResponseCode.InvalidRefreshToken);
            }

            if (refreshToken.IsUsed ||
                refreshToken.ExpirationDate < DateTime.Now)
            {
                throw new AuthenticationException(ResponseCode.InvalidRefreshToken);
            }

            refreshToken.IsUsed = true;

            var newRefreshToken = new RefreshToken
            {
                ExpirationDate = DateTime.Now.AddDays(1),
                IsUsed = false,
                PhoneNumber = refreshToken.PhoneNumber,
                Token = StringHelper.GenerateRandomString(25) + Guid.NewGuid().ToString(),
            };

            await refreshTokenRepository.AddAsync(newRefreshToken);
            await refreshTokenRepository.SaveAsync();

            return new LoginResponseModel
            {
                UserId = refreshToken.Id,
                RefreshToken = refreshToken.Token,
            };
        }
        public async Task<bool?> ChangePhoneNumber(string newPhoneNumber, int? userId)
        {
            if(!StringHelper.IsPhoneNumberValid(newPhoneNumber))
            {
                throw new AuthenticationException(ResponseCode.InvalidPhoneNumber);
            }

            var user = applicationUserRepository.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new AuthenticationException(ResponseCode.UserNotFound);
            }

            if(newPhoneNumber == user.PhoneNumber)
            {
                throw new AuthenticationException(ResponseCode.NewPhoneNumberMatchesOld);
            }

            var dbUser = applicationUserRepository.FirstOrDefault(x => x.PhoneNumber == newPhoneNumber);
            if (dbUser != null)
            {
                throw new AuthenticationException(ResponseCode.AlreadyRegistered);
            }

            user.PhoneNumber = newPhoneNumber;
            await applicationUserRepository.SaveAsync();

            return true;
        }
        public async Task<bool?> ChangeEmail(string email, int? userId)
        {
            if (!StringHelper.IsEmailValid(email))
            {
                throw new AuthenticationException(ResponseCode.InvalidEmailAddress);
            }

            var user = applicationUserRepository.FirstOrDefault(x => x.Id == userId);
            if(user == null)
            {
                throw new AuthenticationException(ResponseCode.UserNotFound);
            }
            
            if(user.Email == email)
            {
                throw new AuthenticationException(ResponseCode.NewEmailMatchesOld);
            }

            var dbUser = applicationUserRepository.FirstOrDefault(x=>x.Email == email);
            if(dbUser != null)
            {
                throw new AuthenticationException(ResponseCode.AlreadyRegistered);
            }

            user.Email = email;
            await applicationUserRepository.SaveAsync();
            return true;
        }     
        
        public async Task<bool?> ChangeFirstName(string firstName, int? userId)
        {
            var user = applicationUserRepository.FirstOrDefault(x => x.Id == userId);
            if(user == null)
            {
                throw new AuthenticationException(ResponseCode.UserNotFound);
            }          

            user.FirstName = firstName;
            await applicationUserRepository.SaveAsync();
            return true;
        }  
        
        public async Task<bool?> ChangeLastName(string LastName, int? userId)
        {
            var user = applicationUserRepository.FirstOrDefault(x => x.Id == userId);
            if(user == null)
            {
                throw new AuthenticationException(ResponseCode.UserNotFound);
            }          

            user.FirstName = LastName;
            await applicationUserRepository.SaveAsync();
            return true;
        }
    }
}
