using Authentication.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : BaseController
    {
        private readonly IRegistrationService registerationService;


        public RegisterController(IRegistrationService registerationService,IConfiguration configuration) : base(configuration) 
        {
            this.registerationService = registerationService;
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        [ProducesResponseType(typeof(BaseResponse<bool?>), 200)]
        public async Task<BaseResponse<bool?>> Register([FromBody]RegisterationModel model)
        {
            var result = await registerationService.Register(model);
            return new BaseResponse<bool?>
            {
                Result = result,
            };
        }
    }
}
