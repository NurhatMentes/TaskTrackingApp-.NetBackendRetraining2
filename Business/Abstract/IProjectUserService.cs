using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IProjectUserService
    {
        IDataResult<ProjectUserDto> GetById(int projectUserId);

        IDataResult<List<ProjectUserDto>> GetAllByProjectId(int projectId);

        IDataResult<List<ProjectUserDto>> GetAllByUserId(int userId);

        IDataResult<List<ProjectUserDto>> GetAll();

        IResult Add(ProjectUserAddDto projectUserAddDto);

        IResult Update(ProjectUserUpdateDto projectUserUpdateDto);

    }
}
