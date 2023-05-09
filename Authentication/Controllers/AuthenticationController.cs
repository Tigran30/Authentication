using Authentication.Base;
using Authentication.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IConfiguration configuration;
        private readonly string secret;

        public AuthenticationController(IAuthenticationService authenticationService, IConfiguration configuration) : base(configuration)
        {
            this.authenticationService = authenticationService;
            this.configuration = configuration;
            this.secret = configuration.GetValue<string>("JwtConfig:Secret");
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(BaseResponse<AuthenticateResponseModel>), 200)]
        public async Task<BaseResponse<AuthenticateResponseModel>> Authenticate(AuthenticateModel model)
        {
            var result = await authenticationService.Login(model);
            var token = JwtHelpers.GenerateToken(result.UserId, secret);
            return new BaseResponse<AuthenticateResponseModel>
            {
                Result = new AuthenticateResponseModel
                {
                    RefreshToken = result.RefreshToken,
                    Token = token
                }
            };
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        [ProducesResponseType(typeof(BaseResponse<AuthenticateResponseModel>), 200)]
        public async Task<BaseResponse<AuthenticateResponseModel>> RefreshToken(string refreshToken)
        {
            var result = await authenticationService.RefreshToken(refreshToken);
            var token = JwtHelpers.GenerateToken(result.UserId, secret);
            return new BaseResponse<AuthenticateResponseModel>
            {
                Result = new AuthenticateResponseModel
                {
                    RefreshToken = result.RefreshToken,
                    Token = token
                }
            };
        }

        [HttpPost("ChangePhoneNumber")]
        [ProducesResponseType(typeof(BaseResponse<bool?>), 200)]
        public async Task<BaseResponse<bool?>> ChangePhoneNumber(string newPhoneNumber)
        {
            var result = await authenticationService.ChangePhoneNumber(newPhoneNumber, ClaimUserId);
            return new BaseResponse<bool?> 
            {
                Result = result
            };
        }
       
        [HttpPost("ChangeEmail")]
        [ProducesResponseType(typeof(BaseResponse<bool?>), 200)]
        public async Task<BaseResponse<bool?>> ChangeEmail(string email)
        {
            var result = await authenticationService.ChangeEmail(email, ClaimUserId);
            return new BaseResponse<bool?> 
            {
                Result = result
            };
        }      
        
        [HttpPost("ChangeFirstName")]
        [ProducesResponseType(typeof(BaseResponse<bool?>), 200)]
        public async Task<BaseResponse<bool?>> ChangeFirstName(string firstName)
        {
            var result = await authenticationService.ChangeFirstName(firstName, ClaimUserId);
            return new BaseResponse<bool?> 
            {
                Result = result
            };
        }  
        
        [HttpPost("ChangeLastName")]
        [ProducesResponseType(typeof(BaseResponse<bool?>), 200)]
        public async Task<BaseResponse<bool?>> ChangeLastName(string lastName)
        {
            var result = await authenticationService.ChangeLastName(lastName, ClaimUserId);
            return new BaseResponse<bool?> 
            {
                Result = result
            };
        }
    }
}
