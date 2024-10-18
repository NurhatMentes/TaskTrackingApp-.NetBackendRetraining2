using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Performance;
using Core.Entities.DTOs;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;


namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private IUserOperationClaimDal _dal;
        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _httpContextAccessor;

        public UserOperationClaimManager(IUserOperationClaimDal dal, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _dal = dal;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        [PerformanceAspect(1)]
        public IDataResult<List<UserOperationClaimDto>> GetClaimsUser()
        {
            return new SuccessDataResult<List<UserOperationClaimDto>>(_dal.GetClaimsUser());
        }

        [PerformanceAspect(1)]
        public IDataResult<List<UserOperationClaimDto>> GetClaimsUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaimDto>>(_dal.GetClaimsUserId(userId));
        }

        [SecuredOperation("Admin")]
        public IResult Update(int userId, int operationClaimId)
        {
            var userOperationClaim = _dal.Get(u => u.UserId == userId);


            var result = BusinessRules.Run(CanUpdateUserRole(userId));

            if (result != null)
            {
                return result;
            }

            userOperationClaim.OperationClaimId = operationClaimId;
            userOperationClaim.UserId = userId;


            _dal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserClaimUpdated);
        }


        //***********BusinessRules
        private IResult CanUpdateUserRole(int userId)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var currentUserId = _tokenHelper.GetUserIdFromToken(token);

            if (currentUserId == userId)
            {
                return new ErrorResult("Kendi rolünüzü değiştiremezsiniz.");
            }

            return new SuccessResult();
        }
    }
}
