using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        List<OperationClaim> GetClaimsUserId(int userId);
        UserDetailDto GetUserDetails(int userId);
    }
}
