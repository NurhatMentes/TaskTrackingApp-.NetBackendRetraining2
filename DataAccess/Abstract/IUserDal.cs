using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        UserDetailDto GetUserDetails(int userId);
        List<OperationClaim> GetClaims(User user);
    }
}
