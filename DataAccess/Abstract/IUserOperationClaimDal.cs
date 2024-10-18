using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
        List<UserOperationClaimDto> GetClaimsUser();
        List<UserOperationClaimDto> GetClaimsUserId(int userId);
    }

}
