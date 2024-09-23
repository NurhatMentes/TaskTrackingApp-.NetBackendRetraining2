using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Task = Entities.Concrete.Task;

namespace Business.Concrete
{
    public class TaskManager : ITaskService
    {
        private ITaskDal _taskDal;
        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _httpContextAccessor;

        public TaskManager(ITaskDal taskDal, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _taskDal = taskDal;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        [SecuredOperation("Admin,Project Manager")]
        [ValidationAspect(typeof(TaskValidator))]
        public IResult Add(TaskAddDto taskAddDto)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var createdByUserId = _tokenHelper.GetUserIdFromToken(token);

            var task = new Task
            {
                ProjectId = taskAddDto.ProjectId,
                Name = taskAddDto.Name,
                Description = taskAddDto.Description,
                CreaterUserId = createdByUserId,
                AssignedUserId = taskAddDto.AssignedUserId,
                Priority = (PriorityLevel)taskAddDto.Priority,
                Status = taskAddDto.Status,
                EndDate = taskAddDto.EndDate,
                CreatedAt = DateTime.Now
            };

            _taskDal.Add(task);
            return new SuccessResult(Messages.TaskAdded);
        }

        [SecuredOperation("Admin,Project Manager")]
        [ValidationAspect(typeof(TaskValidator))]
        public IResult Update(TaskUpdateDto taskUpdateDto)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var updatedByUserId = _tokenHelper.GetUserIdFromToken(token);

            var existingTask = _taskDal.Get(t => t.Id == taskUpdateDto.Id);
            if (existingTask == null)
            {
                return new ErrorResult(Messages.TaskNotFound);
            }

            existingTask.Id = taskUpdateDto.Id;
            existingTask.ProjectId = taskUpdateDto.ProjectId;
            existingTask.UpdatedByUserId = updatedByUserId;
            existingTask.Name = taskUpdateDto.Name;
            existingTask.Description = taskUpdateDto.Description;
            existingTask.AssignedUserId = taskUpdateDto.AssignedUserId == null ? null : taskUpdateDto.AssignedUserId;
            existingTask.Priority = taskUpdateDto.Priority;
            existingTask.Status = taskUpdateDto.Status;
            existingTask.EndDate = taskUpdateDto.EndDate;
            existingTask.UpdatedAt = DateTime.Now;
           

            _taskDal.Update(existingTask);
            return new SuccessResult(Messages.TaskUpdated);
        }

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<TaskDto>> GetAll()
        {
            var tasks = _taskDal.GetAll();
            if (tasks == null)
            {
                return new ErrorDataResult<List<TaskDto>>(Messages.TaskNotFound);
            }
            return new SuccessDataResult<List<TaskDto>>(tasks.Data,Messages.TaskListed);
        }

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<TaskDto>> GetAllByProjectId(int projectId)
        {
            var tasks = _taskDal.GetAllByProjectId(projectId);
            if (tasks == null)
            {
                return new ErrorDataResult<List<TaskDto>>(Messages.TaskNotFound);
            }
            return new SuccessDataResult<List<TaskDto>>(tasks.Data);
        }

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<TaskDto>> GetAllByAssignedUserId(int assignedUserId)
        {
            var tasks = _taskDal.GetAllByAssignedUserId(assignedUserId);
            if (tasks == null)
            {
                return new ErrorDataResult<List<TaskDto>>(Messages.TaskNotFound);
            }
            return new SuccessDataResult<List<TaskDto>>(tasks.Data);
        }

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<TaskDto> GetById(int taskId)
        {
            var task = _taskDal.GetById(taskId);
            if (task == null)
            {
                return new ErrorDataResult<TaskDto>(Messages.TaskNotFound);
            }
            return new SuccessDataResult<TaskDto>(task.Data);
        }
    }

}
