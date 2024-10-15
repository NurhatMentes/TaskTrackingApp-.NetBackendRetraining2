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
        [ValidationAspect(typeof(ProjectUserAddValidator))]
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
        [ValidationAspect(typeof(ProjectUserUpdateValidator))]
        public IResult Update(ProjectUserUpdateDto dto)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var updatedByUserId = _tokenHelper.GetUserIdFromToken(token);

            var existingProjectUser = _projectUserDal.Get(pu => pu.ProjectId == dto.ProjectId && pu.UserId == dto.UserId);

            if (existingProjectUser == null)
            {
                return new ErrorResult(Messages.ProjectUserNotFound);
            }

            if (dto.NewUserId.HasValue)
            {
                var newProjectUserCheck = _projectUserDal.Get(pu => pu.ProjectId == dto.ProjectId && pu.UserId == dto.NewUserId.Value);

         
                if (newProjectUserCheck != null && newProjectUserCheck.Role == dto.Role)
                {
                    return new ErrorResult("Bu kullanıcı zaten projeye atanmış durumda.");
                }

                existingProjectUser.UserId = dto.NewUserId.Value;
            }


            existingProjectUser.Role = dto.Role;
            existingProjectUser.UpdatedByUserId = updatedByUserId;
            existingProjectUser.UpdatedAt = DateTime.Now;

            _projectUserDal.Update(existingProjectUser);

            return new SuccessResult(Messages.ProjectEdit);
        }



        //[CacheAspect(1)]
        [PerformanceAspect(1)]
        public IDataResult<List<ProjectUserDto>> GetAll()
        {
            var projectUsers = _projectUserDal.GetAll();
            return new SuccessDataResult<List<ProjectUserDto>>(projectUsers.Data);
        }

        public IDataResult<List<ProjectUserTaskDto>> GetAllByProjectId(int projectId)
        {
            var project = _projectUserDal.GetAllByProjectId(projectId);

            if (project == null)
            {
                return new ErrorDataResult<List<ProjectUserTaskDto>>(Messages.ProjectNotFound);
            }


            return new SuccessDataResult<List<ProjectUserTaskDto>>(project.Data);
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

        public IDataResult<List<ProjectUsersWithUsersDto>> GetAllProjectsWithUsers()
        {
            var result = _projectUserDal.GetAllProjectsWithUsers();

            if (result.IsSuccess)
            {
                return new SuccessDataResult<List<ProjectUsersWithUsersDto>>(result.Data);
            }

            return new ErrorDataResult<List<ProjectUsersWithUsersDto>>(result.Message);
        }
    }
}
