using Authentication.Base;
using Authentication.Base.Helpers;

namespace Authentication.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        public RegistrationService(IApplicationUserRepository applicationUserRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
        }

        public async Task<bool?> Register(RegisterationModel model)
        {
            if (!StringHelper.IsPhoneNumberValid(model.PhoneNumber))
            {
                throw new AuthenticationException(ResponseCode.InvalidPhoneNumber);
            }

            var user = applicationUserRepository.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);

            if (user != null)
            {
                throw new AuthenticationException(ResponseCode.AlreadyRegistered);
            }

            if (!StringHelper.IsEmailValid(model.Email))
            {
                throw new AuthenticationException(ResponseCode.InvalidEmailAddress);
            }

            var userByEmail = applicationUserRepository.FirstOrDefault(x => x.Email == model.Email);
            if (userByEmail != null)
            {
                throw new AuthenticationException(ResponseCode.EmailAlreadyExists);
            }

            if (model.Password != model.RepeatPassword)
            {
                throw new AuthenticationException(ResponseCode.InvalidEmailAddress);
            }
            user = new Database.Entities.ApplicaitonUser()
            {
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordSalt = StringHelper.GenerateRandomString(30),
            };
            user.PasswordHash = StringHelper.GenerateHash(model.Password, user.PasswordSalt);

            await applicationUserRepository.AddAsync(user);
            await applicationUserRepository.SaveAsync();
            return true;
        }
    }
}
