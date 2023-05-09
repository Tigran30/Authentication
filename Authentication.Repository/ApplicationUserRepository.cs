using Authentication.Base;
using Authentication.Base.Helpers;
using Authentication.Database;
using Authentication.Database.Entities;

namespace Authentication.Repository
{
    public class ApplicationUserRepository : Repository<ApplicaitonUser>, IApplicationUserRepository
    {
        private AuthenticationDbContext authenticationDbContext;

        public ApplicationUserRepository(AuthenticationDbContext authenticationDbContext) : base(authenticationDbContext) 
        {
            this.authenticationDbContext = authenticationDbContext;
        }
    }
}
