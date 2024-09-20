using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IProjectDal : IEntityRepository<Project>
    {
        List<ProjectWithUserDto> GetAllWithUsers();
        ProjectWithUserDto GetByIdWithUser(int projectId);
        ProjectUpdateDto UpdateProjectWithUser(Project project);
    }
}
