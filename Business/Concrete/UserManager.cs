using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Business.ValidationRules.FluentValidation;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Security.Hashing;
using DataAccess.Concrete.EntityFramework;
using Core.Entities.DTOs;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _dal;

        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal dal, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _dal = dal;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult UserAdd(User user)
        {
            _dal.Add(user);
           return new SuccessResult();
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UserValidator))]
        public IDataResult<UserForRegisterDto> Add(UserForRegisterDto userForRegisterDto, string password)
        {
            var result = BusinessRules.Run(CheckIfUserEmailAlreadyExists(userForRegisterDto.Email));

            if (result != null)
            {
                return (IDataResult<UserForRegisterDto>)result;
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _dal.Add(user);

            
            var userForRegisterDtoResponse = new UserForRegisterDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt
            };

            return new SuccessDataResult<UserForRegisterDto>(userForRegisterDtoResponse, Messages.UserRegistered);
        }


        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(UserForUpdateDto userForUpdateDto, int userId)
        {
            var existingUser = _dal.Get(p => p.Id == userId);
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var currentUserId = _tokenHelper.GetUserIdFromToken(token);
            var currentUser = _dal.Get(p => p.Id == currentUserId);

            var result = BusinessRules.Run(CheckIfUserExists(existingUser.Id), CheckIfUserCanChangeStatus(currentUser, userForUpdateDto));

            if (result != null)
            {
                return result;
            }

            existingUser.FirstName = userForUpdateDto.FirstName;
            existingUser.LastName = userForUpdateDto.LastName;
            existingUser.Status = userForUpdateDto.Status;
            existingUser.UpdatedAt = DateTime.UtcNow;

            _dal.Update(existingUser);
            return new SuccessResult(Messages.UserUpdated);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult UpdatePassword(int userId, string currentPassword, string newPassword)
        {
            var existingUser = _dal.Get(p => p.Id == userId);

            var result = BusinessRules.Run(CheckIfUserExists(userId), CheckIfOldPasswordIsValid(existingUser, currentPassword));

            if (result != null)
            {
                return result;
            }


            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
            existingUser.PasswordHash = passwordHash;
            existingUser.PasswordSalt = passwordSalt;

            _dal.Update(existingUser);
            return new SuccessResult(Messages.PasswordUpdated);
        }



        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_dal.GetAll());
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_dal.Get(p => p.Id == userId));
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_dal.Get(p=>p.Email==email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_dal.GetClaims(user));          
        }

      


        //**************Business Rules**************
        private IResult CheckIfUserEmailAlreadyExists(string email)
        {
            var result = _dal.GetAll(x => x.Email == email).Any();
            if (result)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfUserCanChangeStatus(User currentUser, UserForUpdateDto userForUpdateDto)
        {
            var userClaims = _dal.GetClaims(currentUser);
            var isAdmin = userClaims.Any(c => c.OperationClaimName == "Admin");

            if (!isAdmin && currentUser.Status != userForUpdateDto.Status)
            {
                return new ErrorResult(Messages.CheckIfUserIsAdmin  );
            }

            return new SuccessResult();
        }

        private IResult CheckIfUserExists(int userId)
        {
            var existingUser = _dal.Get(p => p.Id == userId);
            if (existingUser == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            return new SuccessResult();
        }


        private IResult CheckIfOldPasswordIsValid(User existingUser, string currentPassword)
        {
            if (!HashingHelper.VerifyPasswordHash(currentPassword, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }

            return new SuccessResult();
        }

    }
}
