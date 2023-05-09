using Authentication.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authentication.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public BaseController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int? ClaimUserId
        {
            get
            {
                return GetClaim(JwtHelpers.UserIdClaim);
            }
        }

        private int? GetClaim(string claim)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaim = identity.FindFirst(claim);
                int.TryParse(userClaim.Value, out var userId);
                return userId;
            }
            return null;
        }
    }
}
