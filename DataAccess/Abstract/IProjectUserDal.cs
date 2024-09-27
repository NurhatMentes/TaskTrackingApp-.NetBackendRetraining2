using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IProjectUserDal :IEntityRepository<ProjectUser>
    {
        IDataResult<List<ProjectUserDto>> GetAll();
        IDataResult<List<ProjectUserDto>> GetAllByUserId(int userId);
        IDataResult<List<ProjectUserDto>> GetAllByProjectId(int projectId);
        IDataResult<ProjectUserDto> GetById(int projectUserId);
        IDataResult<List<ProjectUsersWithUsersDto>> GetAllProjectsWithUsers();
    }
}
