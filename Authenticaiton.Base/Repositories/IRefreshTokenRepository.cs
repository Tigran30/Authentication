using Authentication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Base
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken entity);
        Task SaveAsync();
        RefreshToken? FirstOrDefault(Expression<Func<RefreshToken, bool>> predicate);
        IQueryable<RefreshToken> GetAll();

    }
}
