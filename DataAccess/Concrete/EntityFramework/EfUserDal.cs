using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, TaskTrackingAppDBContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, OperationClaimName = operationClaim.OperationClaimName };
                return result.ToList();

            }
        }

        public List<OperationClaim> GetClaimsUserId(int userId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == userId
                             select new OperationClaim { Id = operationClaim.Id, OperationClaimName = operationClaim.OperationClaimName };
                return result.ToList();

            }
        }

        public UserDetailDto GetUserDetails(int userId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from user in context.Users
                             where user.Id == userId
                             join userOperationClaim in context.UserOperationClaims on user.Id equals userOperationClaim.UserId into userClaims
                             from userClaim in userClaims.DefaultIfEmpty()
                             join operationClaim in context.OperationClaims on userClaim.OperationClaimId equals operationClaim.Id into operationClaims
                             from claim in operationClaims.DefaultIfEmpty()
                             join projectUser in context.ProjectUsers on user.Id equals projectUser.UserId into projectUsers
                             from projUser in projectUsers.DefaultIfEmpty()
                             join project in context.Projects on projUser.ProjectId equals project.Id into projects
                             from proj in projects.DefaultIfEmpty()
                             join task in context.Tasks on proj.Id equals task.ProjectId into tasks
                             from t in tasks.DefaultIfEmpty()
                             select new UserDetailDto
                             {
                                 UserId = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 Role = claim.OperationClaimName,
                                 ProjectName = proj.Name,
                                 ProjectStartDate = proj.StartDate,
                                 ProjectEndDate = proj.EndDate,
                                 TaskName = t != null ? t.Name : null,
                                 AssignedBy = t.AssignedUserId != null && t.AssignedUser != null
                                 ? t.AssignedUser.FirstName + " " + t.AssignedUser.LastName : null,
                                 TaskStartDate = t.CreatedAt,
                                 TaskEndDate = t.EndDate,
                                 OnlineStatus =  user.OnlineStatus
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
