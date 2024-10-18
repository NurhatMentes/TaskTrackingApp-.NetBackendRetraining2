using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, TaskTrackingAppDBContext>, IUserOperationClaimDal
    {
        public List<UserOperationClaimDto> GetClaimsUser()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from userOperationClaim in context.UserOperationClaims
                             join user in context.Users
                                 on userOperationClaim.UserId equals user.Id
                                 join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                             select new UserOperationClaimDto {
                                 UserName = user.FirstName + " " + user.LastName,
                                 Email = user.Email,
                                 OperationClaimName = operationClaim.OperationClaimName
                             };
                return result.ToList();

            }
        }

        public List<UserOperationClaimDto> GetClaimsUserId(int userId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             join user in context.Users on userOperationClaim.UserId equals user.Id
                             where userOperationClaim.UserId == userId
                             select new UserOperationClaimDto {
                                 UserName = user.FirstName + " " + user.LastName,
                                 Email = user.Email,
                                 OperationClaimName = operationClaim.OperationClaimName
                             };
                return result.ToList();

            }
        }
    }

}
