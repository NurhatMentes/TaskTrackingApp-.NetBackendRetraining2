using Core.Constants;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProjectUserDal : EfEntityRepositoryBase<ProjectUser, TaskTrackingAppDBContext>, IProjectUserDal
    {
        public IDataResult<List<ProjectUserDto>> GetAll()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUsers = from pu in context.ProjectUsers
                                   join p in context.Projects on pu.ProjectId equals p.Id
                                   join u in context.Users on pu.UserId equals u.Id
                                   join updaterUser in context.Users on pu.UpdatedByUserId equals updaterUser.Id into updaterGroup
                                   from updaterUser in updaterGroup.DefaultIfEmpty()
                                   select new ProjectUserDto
                                   {
                                       Id = pu.Id,
                                       ProjectId = pu.ProjectId,
                                       UserId = pu.UserId,
                                       UpdatedByUserId = pu.UpdatedByUserId,
                                       UpdatedByUserName = updaterUser.FirstName + " " + updaterUser.LastName,
                                       UpdatedByUserEmail = updaterUser.Email,
                                       Role = pu.Role,
                                       ProjectName = p.Name, 
                                       UserName = u.FirstName + " " + u.LastName, 
                                       UserEmail = u.Email,
                                       UpdatedAt = pu.UpdatedAt
                                   };

                return new SuccessDataResult<List<ProjectUserDto>>(projectUsers.ToList());
            }
        }

        public IDataResult<List<ProjectUserDto>> GetAllByProjectId(int projectId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUsers = (from pu in context.ProjectUsers
                                    join p in context.Projects on pu.ProjectId equals p.Id
                                    join u in context.Users on pu.UserId equals u.Id
                                    join updaterUser in context.Users on pu.UpdatedByUserId equals updaterUser.Id into updaterGroup
                                    from updaterUser in updaterGroup.DefaultIfEmpty()
                                    where pu.ProjectId == projectId
                                    select new ProjectUserDto
                                    {
                                        Id = pu.Id,
                                        ProjectId = pu.ProjectId,
                                        UserId = pu.UserId,
                                        UpdatedByUserId = pu.UpdatedByUserId,
                                        UpdatedByUserName = updaterUser.FirstName + " " + updaterUser.LastName,
                                        UpdatedByUserEmail = updaterUser.Email,
                                        Role = pu.Role,
                                        ProjectName = p.Name,
                                        UserName = u.FirstName + " " + u.LastName,
                                        UserEmail = u.Email,
                                        UpdatedAt = pu.UpdatedAt
                                    }).ToList();

                if (!projectUsers.Any())
                {
                    return new ErrorDataResult<List<ProjectUserDto>>("Proje bulunamadı.");
                }

                return new SuccessDataResult<List<ProjectUserDto>>(projectUsers);
            }
        }

        public IDataResult<List<ProjectUserDto>> GetAllByUserId(int userId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUsers = from pu in context.ProjectUsers
                                   join p in context.Projects on pu.ProjectId equals p.Id
                                   join u in context.Users on pu.UserId equals u.Id
                                   join updaterUser in context.Users on pu.UpdatedByUserId equals updaterUser.Id into updaterGroup
                                   from updaterUser in updaterGroup.DefaultIfEmpty()
                                   where pu.UserId == userId
                                   select new ProjectUserDto
                                   {
                                       Id = pu.Id,
                                       ProjectId = pu.ProjectId,
                                       UserId = pu.UserId,
                                       UpdatedByUserId = pu.UpdatedByUserId,
                                       UpdatedByUserName = updaterUser.FirstName + " " + updaterUser.LastName,
                                       UpdatedByUserEmail = updaterUser.Email,
                                       Role = pu.Role,
                                       ProjectName = p.Name,
                                       UserName = u.FirstName + " " + u.LastName,
                                       UserEmail = u.Email,
                                       UpdatedAt = pu.UpdatedAt
                                   };

                return new SuccessDataResult<List<ProjectUserDto>>(projectUsers.ToList());
            }
        }

        public IDataResult<ProjectUserDto> GetById(int projectUserId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUser = (from pu in context.ProjectUsers
                                   join p in context.Projects on pu.ProjectId equals p.Id
                                   join u in context.Users on pu.UserId equals u.Id
                                   join updaterUser in context.Users on pu.UpdatedByUserId equals updaterUser.Id into updaterGroup
                                   from updaterUser in updaterGroup.DefaultIfEmpty()
                                   where pu.Id == projectUserId
                                   select new ProjectUserDto
                                   {
                                       Id = pu.Id,
                                       ProjectId = pu.ProjectId,
                                       UserId = pu.UserId,
                                       UpdatedByUserId = pu.UpdatedByUserId,
                                       UpdatedByUserName = updaterUser.FirstName + " " + updaterUser.LastName,
                                       UpdatedByUserEmail = updaterUser.Email,
                                       Role = pu.Role,
                                       ProjectName = p.Name,
                                       UserName = u.FirstName + " " + u.LastName,
                                       UserEmail = u.Email,
                                       UpdatedAt = pu.UpdatedAt
                                   }).FirstOrDefault();

                if (projectUser != null)
                {
                    return new SuccessDataResult<ProjectUserDto>(projectUser);
                }

                return new ErrorDataResult<ProjectUserDto>("Proje kullanıcısı bulunamadı.");
            }
        }

        public IDataResult<List<ProjectUsersWithUsersDto>> GetAllProjectsWithUsers()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = (from pu in context.ProjectUsers
                              join p in context.Projects on pu.ProjectId equals p.Id
                              join u in context.Users on pu.UserId equals u.Id
                              select new ProjectUsersWithUsersDto 
                              {
                                  ProjectId = p.Id,
                                  ProjectName = p.Name,
                                  UserId = u.Id,
                                  UserName = u.FirstName + " " + u.LastName,
                                  UserEmail = u.Email,
                                  Role = pu.Role,
                                  UpdatedAt = pu.UpdatedAt
                              })
                             .ToList(); 

                if (result != null && result.Count > 0)
                {
                    return new SuccessDataResult<List<ProjectUsersWithUsersDto>>(result);
                }

                return new ErrorDataResult<List<ProjectUsersWithUsersDto>>("Proje ve kullanıcı verileri bulunamadı.");
            }
        }



    }
}
