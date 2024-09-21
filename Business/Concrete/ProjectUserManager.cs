using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class ProjectUserManager : IProjectUserService
    {
        private IProjectUserDal _projectUserDal;

        public ProjectUserManager(IProjectUserDal projectUserDal)
        {
            _projectUserDal = projectUserDal;
        }

        public IResult Add(ProjectUserAddDto projectUserAddDto)
        {
            var projectUser = new ProjectUser
            {
                ProjectId = projectUserAddDto.ProjectId,
                UserId = projectUserAddDto.UserId,
                Role = projectUserAddDto.Role
            };

            _projectUserDal.Add(projectUser); 
            return new SuccessResult(Messages.ProjectUserAdded);
        }

        public IResult Update(ProjectUser projectUser)
        {
            var existingProjectUser = _projectUserDal.Get(pu => pu.Id == projectUser.Id);
            if (existingProjectUser == null)
            {
                return new ErrorResult(Messages.ProjectUserNotFound);
            }

            _projectUserDal.Update(projectUser);
            return new SuccessResult(Messages.ProjectUserUpdated);
        }

        public IDataResult<List<ProjectUserDto>> GetAll()
        {
            var projectUsers = _projectUserDal.GetAll();
            return new SuccessDataResult<List<ProjectUserDto>>(projectUsers.Data);
        }

        public IDataResult<List<ProjectUserDto>> GetAllByProjectId(int projectId)
        {
            var project = _projectUserDal.GetAllByProjectId(projectId);

            if (project == null)
            {
                return new ErrorDataResult<List<ProjectUserDto>>(Messages.ProjectNotFound);
            }


            return new SuccessDataResult<List<ProjectUserDto>>(project.Data);
        }



        public IDataResult<List<ProjectUserDto>> GetAllByUserId(int userId)
        {
            var projectUsers = _projectUserDal.GetAllByUserId(userId).Data;

            if (projectUsers == null)
            {
                return new ErrorDataResult<List<ProjectUserDto>>(Messages.ProjectUserNotFound);
            }

           
            return new SuccessDataResult<List<ProjectUserDto>>(projectUsers);
        }

        public IDataResult<ProjectUserDto> GetById(int projectUserId)
        {
            var project = _projectUserDal.GetById(projectUserId).Data;

            if (project == null)
            {
                return new ErrorDataResult<ProjectUserDto>(Messages.ProjectUserNotFound);
            }

            return new SuccessDataResult<ProjectUserDto>(project);
        }
    }
}
