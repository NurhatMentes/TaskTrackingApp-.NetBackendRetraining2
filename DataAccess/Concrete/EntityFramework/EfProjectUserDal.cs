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

        public IDataResult<List<ProjectUserTaskDto>> GetAllByProjectId(int projectId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectUsers = (from pu in context.ProjectUsers
                                    join p in context.Projects on pu.ProjectId equals p.Id
                                    join u in context.Users on pu.UserId equals u.Id
                                    join t in context.Tasks on pu.ProjectId equals t.ProjectId into tp
                                    from t in tp.DefaultIfEmpty()
                                    join updaterUser in context.Users on pu.UpdatedByUserId equals updaterUser.Id into updaterGroup
                                    from updaterUser in updaterGroup.DefaultIfEmpty()
                                    where pu.ProjectId == projectId
                                    select new
                                    {
                                        ProjectUser = pu,
                                        Project = p,
                                        User = u,
                                        Task = t,
                                        UpdatedByUser = updaterUser
                                    })
                                    .GroupBy(x => new { x.ProjectUser.UserId, x.ProjectUser.ProjectId })
                                    .Select(g => new ProjectUserTaskDto
                                    {
                                        Id = g.First().ProjectUser.Id,
                                        ProjectId = g.Key.ProjectId,
                                        UserId = g.Key.UserId,
                                        UpdatedByUserId = g.First().ProjectUser.UpdatedByUserId,
                                        UpdatedByUserName = g.First().UpdatedByUser != null ? g.First().UpdatedByUser.FirstName + " " + g.First().UpdatedByUser.LastName : null,
                                        UpdatedByUserEmail = g.First().UpdatedByUser != null ? g.First().UpdatedByUser.Email : null,
                                        Role = g.First().ProjectUser.Role,
                                        ProjectName = g.First().Project.Name,
                                        ProjectDescription = g.First().Project.Description,
                                        ProjectEndDate = (DateTime)g.First().Project.EndDate,
                                        ProjectStartDate = g.First().Project.StartDate,
                                        ProjectStatus = g.First().Project.Status,
                                        UserName = g.First().User.FirstName + " " + g.First().User.LastName,
                                        UserEmail = g.First().User.Email,
                                        UpdatedAt = g.First().ProjectUser.UpdatedAt,
                                      
                                        Tasks = g.Where(t => t.Task != null && t.Task.AssignedUserId == g.Key.UserId)
                                                .Select(t => new TaskDto
                                                {
                                                    Id = t.Task.Id,
                                                    Name = t.Task.Name,
                                                    AssignedUserId = t.Task.AssignedUserId,
                                                    Description = t.Task.Description,
                                                    Priority = t.Task.Priority,
                                                    Status = t.Task.Status,
                                                    EndDate = t.Task.EndDate  ,
                                                    AssignedUserName = t.Task.AssignedUser.FirstName + " " + t.Task.AssignedUser.LastName
                                                }).ToList()
                                    }).ToList();

                if (!projectUsers.Any())
                {
                    return new ErrorDataResult<List<ProjectUserTaskDto>>("Proje bulunamadı.");
                }

                return new SuccessDataResult<List<ProjectUserTaskDto>>(projectUsers);
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
