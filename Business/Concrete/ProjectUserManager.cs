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

namespace Business.Concrete
{
    public class ProjectUserManager : IProjectUserService
    {
        private IProjectUserDal _projectUserDal;
        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _httpContextAccessor;

        public ProjectUserManager(IProjectUserDal projectUserDal, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _projectUserDal = projectUserDal;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        [SecuredOperation("Admin,Project Manager")]
        [ValidationAspect(typeof(ProjectUserValidator))]
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

        [SecuredOperation("Admin,Project Manager")]
        [ValidationAspect(typeof(ProjectUserValidator))]
        public IResult Update(ProjectUserUpdateDto dto)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var updatedByUserId = _tokenHelper.GetUserIdFromToken(token);

            var existingProjectUser = _projectUserDal.Get(pu => pu.Id == dto.ProjectId);
            if (existingProjectUser == null)
            {
                return new ErrorResult(Messages.ProjectUserNotFound);
            }

             existingProjectUser.ProjectId = dto.ProjectId;
            existingProjectUser.UpdatedByUserId = updatedByUserId;
            existingProjectUser.UserId = dto.UserId;
            existingProjectUser.Role = dto.Role;

            _projectUserDal.Update(existingProjectUser);
            return new SuccessResult(Messages.ProjectUserUpdated);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
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
