using Core.DataAccess;
using Core.Utilities.Results;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ITaskDal : IEntityRepository<Entities.Concrete.Task>
    {
        IDataResult<List<TaskDto>> GetAll();
        IDataResult<List<TaskDto>> GetAllByProjectId(int projectId);
        IDataResult<List<TaskDto>> GetAllByAssignedUserId(int assignedUserId);
        IDataResult<TaskDto> GetById(int taskId);
    }
}
