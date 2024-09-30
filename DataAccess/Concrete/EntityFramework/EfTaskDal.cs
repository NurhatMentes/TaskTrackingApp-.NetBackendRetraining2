using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTaskDal : EfEntityRepositoryBase<Entities.Concrete.Task, TaskTrackingAppDBContext>, ITaskDal
    {
        public IDataResult<List<TaskDto>> GetAll()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var tasks = from t in context.Tasks
                            join p in context.Projects on t.ProjectId equals p.Id
                            join u in context.Users on t.AssignedUserId equals u.Id into userGroup
                            from u in userGroup.DefaultIfEmpty()
                            join a in context.Users on t.CreaterUserId equals a.Id
                            select new TaskDto
                            {
                                Id = t.Id,
                                ProjectId = t.ProjectId,
                                Name = t.Name,
                                Description = t.Description,
                                ProjectName = p.Name,
                                CreaterUserId = t.CreaterUserId,
                                CreaterUserName = a.FirstName + " " + a.LastName,
                                AssignedUserId = t.AssignedUserId,
                                AssignedUserName = u != null ? u.FirstName + " " + u.LastName : null,
                                UpdatedByUserId =  t.UpdatedByUserId,
                                UpdatedByUserName = u != null ? u.FirstName + " " + u.LastName : null,
                                Priority = t.Priority,
                                Status = t != null ? t.Status : null,
                                EndDate = t.EndDate,
                                CreatedAt = t.CreatedAt,
                                UpdatedAt = t.UpdatedAt
                            };

                return new SuccessDataResult<List<TaskDto>>(tasks.ToList());
            }
        }

        public IDataResult<List<TaskDto>> GetAllByProjectId(int projectId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var tasks = from t in context.Tasks
                            join p in context.Projects on t.ProjectId equals p.Id
                            where t.ProjectId == projectId
                            join a in context.Users on t.CreaterUserId equals a.Id
                            join u in context.Users on t.AssignedUserId equals u.Id into userGroup
                            from u in userGroup.DefaultIfEmpty()
                            select new TaskDto
                            {
                                Id = t.Id,
                                ProjectId = t.ProjectId,
                                Name = t.Name,
                                Description = t.Description,
                                ProjectName = p.Name,
                                CreaterUserId = t.CreaterUserId,
                                CreaterUserName = a.FirstName + " " + a.LastName,
                                AssignedUserId = t.AssignedUserId,
                                AssignedUserName = u != null ? u.FirstName + " " + u.LastName : null,
                                UpdatedByUserId = t.UpdatedByUserId,
                                UpdatedByUserName = u != null ? u.FirstName + " " + u.LastName : null,
                                Priority = t.Priority,
                                Status = t != null ? t.Status : null,
                                EndDate = t.EndDate,
                                CreatedAt = t.CreatedAt ,
                                UpdatedAt = t.UpdatedAt
                            };

                return new SuccessDataResult<List<TaskDto>>(tasks.ToList());
            }
        }

        public IDataResult<List<TaskDto>> GetAllByAssignedUserId(int assignedUserId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var tasks = from t in context.Tasks
                            join p in context.Projects on t.ProjectId equals p.Id
                            where t.AssignedUserId == assignedUserId
                            join a in context.Users on t.CreaterUserId equals a.Id
                            join u in context.Users on t.AssignedUserId equals u.Id into userGroup
                            from u in userGroup.DefaultIfEmpty()

                            select new TaskDto
                            {
                                Id = t.Id,
                                ProjectId = t.ProjectId,
                                Name = t.Name,
                                Description = t.Description,
                                ProjectName = p.Name,
                                CreaterUserId = t.CreaterUserId,
                                CreaterUserName = a.FirstName + " " + a.LastName,
                                AssignedUserId = t.AssignedUserId,
                                AssignedUserName = u != null ? u.FirstName + " " + u.LastName : null,
                                UpdatedByUserId = t.UpdatedByUserId,
                                UpdatedByUserName = u != null ? u.FirstName + " " + u.LastName : null,
                                Priority = t.Priority,
                                Status = t != null ? t.Status : null,
                                EndDate = t.EndDate,
                                CreatedAt = t.CreatedAt,     
                                UpdatedAt = t.UpdatedAt
                            };

                return new SuccessDataResult<List<TaskDto>>(tasks.ToList());
            }
        }

        public IDataResult<TaskDto> GetById(int taskId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var task = (from t in context.Tasks
                            join p in context.Projects on t.ProjectId equals p.Id
                            join u in context.Users on t.AssignedUserId equals u.Id into userGroup
                            from u in userGroup.DefaultIfEmpty()
                            join a in context.Users on t.CreaterUserId equals a.Id
                            where t.Id == taskId
                            select new TaskDto
                            {
                                Id = t.Id,
                                ProjectId = t.ProjectId,
                                Name = t.Name,
                                Description = t.Description,
                                ProjectName = p.Name,
                                CreaterUserId = t.CreaterUserId,
                                CreaterUserName = a.FirstName + " " + a.LastName,
                                AssignedUserId = t.AssignedUserId,
                                AssignedUserName = u != null ? u.FirstName + " " + u.LastName : null,
                                UpdatedByUserId = t.UpdatedByUserId,
                                UpdatedByUserName = u != null ? u.FirstName + " " + u.LastName : null,
                                Priority = t.Priority,
                                Status = t != null ? t.Status : null,
                                EndDate = t.EndDate,
                                CreatedAt = t.CreatedAt,
                                UpdatedAt = t.UpdatedAt 
                            }).FirstOrDefault();

                if (task != null)
                {
                    return new SuccessDataResult<TaskDto>(task);
                }

                return new ErrorDataResult<TaskDto>("Görev bulunamadı.");
            }
        }
    }

}
