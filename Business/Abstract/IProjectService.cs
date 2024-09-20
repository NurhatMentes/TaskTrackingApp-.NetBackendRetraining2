using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProjectService
    {
        IDataResult<ProjectWithUserDto> GetById(int projectId); 
        IDataResult<List<ProjectWithUserDto>> GetAll(); 
        IResult Add(ProjectAddDto projectAddDto); 
        IResult Update(ProjectUpdateDto projectDto);
        IResult ChangeStatus(int projectId, bool status);
    }
}
