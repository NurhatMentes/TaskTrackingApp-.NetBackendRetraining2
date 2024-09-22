using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITaskService
    {
        IDataResult<TaskDto> GetById(int taskId);
        IDataResult<List<TaskDto>> GetAllByProjectId(int projectId);
        IDataResult<List<TaskDto>> GetAllByAssignedUserId(int assignedUserId);
        IDataResult<List<TaskDto>> GetAll();
        IResult Add(TaskAddDto taskAddDto);
        IResult Update(TaskUpdateDto taskUpdateDto);
    }

}
