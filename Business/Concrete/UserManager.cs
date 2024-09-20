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
using Core.Entities.DTOs;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IUserOperationClaimDal _userOperationClaimDal;


        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal dal, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor, IUserOperationClaimDal operationClaimDal)
        {
            _userDal = dal;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
            _userOperationClaimDal = operationClaimDal;    
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult UserAdd(User user)
        {
            _userDal.Add(user);
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

            _userDal.Add(user);

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
            var existingUser = _userDal.Get(p => p.Id == userId);
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var currentUserId = _tokenHelper.GetUserIdFromToken(token);
            var currentUser = _userDal.Get(p => p.Id == currentUserId);

            var result = BusinessRules.Run(CheckIfUserExists(existingUser.Id), CheckIfUserCanChangeStatus(currentUser, userForUpdateDto));

            if (result != null)
            {
                return result;
            }

            existingUser.FirstName = userForUpdateDto.FirstName;
            existingUser.LastName = userForUpdateDto.LastName;
            existingUser.Status = userForUpdateDto.Status;
            existingUser.UpdatedAt = DateTime.UtcNow;

            _userDal.Update(existingUser);
            return new SuccessResult(Messages.UserUpdated);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult UpdatePassword(int userId, string currentPassword, string newPassword)
        {
            var existingUser = _userDal.Get(p => p.Id == userId);

            var result = BusinessRules.Run(CheckIfUserExists(userId), CheckIfOldPasswordIsValid(existingUser, currentPassword));

            if (result != null)
            {
                return result;
            }


            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
            existingUser.PasswordHash = passwordHash;
            existingUser.PasswordSalt = passwordSalt;

            _userDal.Update(existingUser);
            return new SuccessResult(Messages.PasswordUpdated);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult UpdateUserRole(int userId, int operationClaimId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var userOperationClaim = _userOperationClaimDal.Get(uoc => uoc.UserId == userId);
            if (userOperationClaim == null)
            {
                return new ErrorResult(Messages.UserRoleNotFound);
            }

            userOperationClaim.OperationClaimId = operationClaimId;
            _userOperationClaimDal.Update(userOperationClaim);

            return new SuccessResult(Messages.UserRoleUpdated);
        }



        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Id == userId));
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(p=>p.Email==email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));          
        }
        public IDataResult<UserDetailDto> GetUserDetail(int userId)
        {
            return new SuccessDataResult<UserDetailDto>(_userDal.GetUserDetails(userId));
        }




        //**************Business Rules**************
        private IResult CheckIfUserEmailAlreadyExists(string email)
        {
            var result = _userDal.GetAll(x => x.Email == email).Any();
            if (result)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfUserCanChangeStatus(User currentUser, UserForUpdateDto userForUpdateDto)
        {
            var userClaims = _userDal.GetClaims(currentUser);
            var isAdmin = userClaims.Any(c => c.OperationClaimName == "Admin");

            if (!isAdmin && currentUser.Status != userForUpdateDto.Status)
            {
                return new ErrorResult(Messages.CheckIfUserIsAdmin  );
            }

            return new SuccessResult();
        }

        private IResult CheckIfUserExists(int userId)
        {
            var existingUser = _userDal.Get(p => p.Id == userId);
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
