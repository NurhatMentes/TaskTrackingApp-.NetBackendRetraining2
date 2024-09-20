using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProjectDal : EfEntityRepositoryBase<Project,TaskTrackingAppDBContext>,IProjectDal
    {
        public List<ProjectWithUserDto> GetAllWithUsers()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectsWithUsers = from project in context.Projects
                                        join creatorUser in context.Users on project.CreatedByUserId equals creatorUser.Id into creatorGroup
                                        from creatorUser in creatorGroup.DefaultIfEmpty()
                                        join updaterUser in context.Users on project.UpdatedByUserId equals updaterUser.Id into updaterGroup
                                        from updaterUser in updaterGroup.DefaultIfEmpty()
                                        select new ProjectWithUserDto
                                        {
                                            Id = project.Id,
                                            Name = project.Name,
                                            Description = project.Description,
                                            StartDate = project.StartDate,
                                            EndDate = project.EndDate,
                                            Status = project.Status,
                                            CreatedByUserEmail = creatorUser.Email,
                                            CreatedByUserName = creatorUser.FirstName + " " + creatorUser.LastName,
                                            UpdatedByUserEmail = updaterUser != null ? updaterUser.Email : null,
                                            UpdatedByUserName = updaterUser != null ? updaterUser.FirstName + " " + updaterUser.LastName : null
                                        };

                return projectsWithUsers.ToList();
            }
        }
        public ProjectWithUserDto GetByIdWithUser(int projectId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var projectWithUser = (from project in context.Projects
                                       join user in context.Users on project.CreatedByUserId equals user.Id into userGroup
                                       from user in userGroup.DefaultIfEmpty()
                                       where project.Id == projectId
                                       select new ProjectWithUserDto
                                       {
                                           Id = project.Id,
                                           Name = project.Name,
                                           Description = project.Description,
                                           StartDate = project.StartDate,
                                           EndDate = project.EndDate,
                                           Status = project.Status,
                                           CreatedByUserEmail = user.Email,
                                           CreatedByUserName = user.FirstName + " " + user.LastName
                                       }).FirstOrDefault();

                return projectWithUser;
            }
        }
        public ProjectUpdateDto UpdateProjectWithUser(Project project)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                context.Projects.Update(project);
                context.SaveChanges();

                var updatedProject = (from proj in context.Projects
                                      join user in context.Users on proj.CreatedByUserId equals user.Id into userGroup
                                      from user in userGroup.DefaultIfEmpty()
                                      where proj.Id == project.Id
                                      select new ProjectUpdateDto
                                      {
                                          Id = proj.Id,
                                          Name = proj.Name,
                                          Description = proj.Description,
                                          StartDate = proj.StartDate,
                                          EndDate = (DateTime)proj.EndDate,
                                          Status = proj.Status,
                                          UpdatedByUserEmail = user.Email,
                                          UpdatedByUserName = user.FirstName + " " + user.LastName,
                                      }).FirstOrDefault();

                return updatedProject;
            }
        }
    }
}
