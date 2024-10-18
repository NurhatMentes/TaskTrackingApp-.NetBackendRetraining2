using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaimDto>> GetClaimsUser();
        IDataResult<List<UserOperationClaimDto>> GetClaimsUserId(int userId);
        IResult Update(int userId, int operationClaimId);
    }
}
