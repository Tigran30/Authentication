using Authentication.Database.Entities;
using System.Linq.Expressions;

namespace Authentication.Base
{
    public interface IApplicationUserRepository
    {
        Task AddAsync(ApplicaitonUser entity);
        Task SaveAsync();
        ApplicaitonUser? FirstOrDefault(Expression<Func<ApplicaitonUser, bool>> predicate);
    }
}
