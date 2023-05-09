using Authentication.Base;
using Authentication.Database.Entities;
using Authentication.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Repository
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        private AuthenticationDbContext authenticationDbContext;

        public RefreshTokenRepository(AuthenticationDbContext authenticationDbContext) : base(authenticationDbContext)
        {
            this.authenticationDbContext = authenticationDbContext;
        }
    }
}
