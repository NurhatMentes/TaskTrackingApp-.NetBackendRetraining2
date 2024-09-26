using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;


namespace Business.Concrete
{
    public class ProjectManager : IProjectService
    {
        private IProjectDal _projectDal;
        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _httpContextAccessor;

        public ProjectManager(IProjectDal dal, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _projectDal = dal;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        [CacheAspect(1)]
        [PerformanceAspect(1)]
        public IDataResult<List<ProjectWithUserDto>> GetAll()
        {
            var projectsWithUsers = _projectDal.GetAllWithUsers();
            return new SuccessDataResult<List<ProjectWithUserDto>>(projectsWithUsers);
        }

        public IDataResult<ProjectWithUserDto> GetById(int projectId)
        {
            var project = _projectDal.GetByIdWithUser(projectId);
            if (project == null)
            {
                return new ErrorDataResult<ProjectWithUserDto>(Messages.ProjectNotFound);
            }
            return new SuccessDataResult<ProjectWithUserDto>(project);
        }

        [SecuredOperation("Admin,Project Manager")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(ProjectAddDto projectAddDto)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var createdByUserId = _tokenHelper.GetUserIdFromToken(token);

            var result = BusinessRules.Run(
                    CheckIfStartDateIsInThePast(projectAddDto.StartDate),
                         CheckIfEndDateIsNotEarlierOrSameAsStartDate(projectAddDto.StartDate, projectAddDto.EndDate)
                    );

            if (result != null)
            {
                return result;
            }

            var project = new Project
            {
                Name = projectAddDto.ProjectName,
                Description = projectAddDto.Description,
                StartDate = projectAddDto.StartDate,
                EndDate = projectAddDto.EndDate,
                CreatedByUserId = createdByUserId,
                CreatedAt = DateTime.UtcNow,
                Status = projectAddDto.Status
            };

            _projectDal.Add(project);

            return new SuccessResult(Messages.ProjectAdded);
        }

        [SecuredOperation("Admin,Project Manager")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(ProjectUpdateDto projectDto)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var updatedByUserId = _tokenHelper.GetUserIdFromToken(token);

            var existingProject = _projectDal.Get(p => p.Id == projectDto.Id);

            if (existingProject == null)
            {
                return new ErrorResult(Messages.ProjectNotFound);
            }

            var result = BusinessRules.Run(CheckIfEndDateIsNotEarlierOrSameAsStartDate(projectDto.StartDate, projectDto.EndDate));

            if (result != null)
            {
                return result;
            }

            existingProject.Name = projectDto.Name;
            existingProject.Description = projectDto.Description;
            existingProject.StartDate = projectDto.StartDate;
            existingProject.EndDate = projectDto.EndDate;
            existingProject.Status = projectDto.Status;
            existingProject.UpdatedAt = DateTime.Now;
            existingProject.UpdatedByUserId = updatedByUserId;


            _projectDal.Update(existingProject);

            return new SuccessResult(Messages.ProjectUpdated);
        }

        [SecuredOperation("Admin,Project Manager")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult ChangeStatus(int projectId, bool status)
        {
            var existingProject = _projectDal.Get(p => p.Id == projectId);
            if (existingProject == null)
            {
                return new ErrorResult(Messages.ProjectNotFound);
            }

            existingProject.Status = status;
            _projectDal.Update(existingProject);
            return new SuccessResult(Messages.ProjectStatusChanged);
        }


        //************Busines Rules***********
        private IResult CheckIfStartDateIsInThePast(DateTime startDate)
        {
            if (startDate < DateTime.Today)
            {
                return new ErrorResult(Messages.StartDateCannotBeInThePast);
            }
            return new SuccessResult();
        }

        private IResult CheckIfEndDateIsNotEarlierOrSameAsStartDate(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
            {
                return new ErrorResult(Messages.EndDateCannotBeEarlierOrSameAsStartDate);
            }
            return new SuccessResult();
        }
    }
}
