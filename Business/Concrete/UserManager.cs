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

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _dal;

        public UserManager(IUserDal dal)
        {
            _dal = dal;
        }


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

            // UserForRegisterDto nesnesini oluşturma
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
        public IResult Update(User user, string currentPassword)
        {
            var existingUser = _dal.Get(p => p.Id == user.Id);

      
            var isAdminResult = CheckIfUserIsAdmin(user.Id);
            var isOldPasswordValidResult = CheckIfOldPasswordIsValid(existingUser, currentPassword);

            var result = BusinessRules.Run(isAdminResult, isOldPasswordValidResult);

            if (result != null)
            {
                return result;
            }

            if (!string.IsNullOrEmpty(user.NewPassword))
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(user.NewPassword, out passwordHash, out passwordSalt);
                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;
                user.NewPassword = null;
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Status = user.Status;
            existingUser.UpdatedAt = DateTime.UtcNow;

            _dal.Update(existingUser);
            return new SuccessResult(Messages.UserUpdated);
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

        private IResult CheckIfUserIsAdmin(int userId)
        {
            var userClaims = _dal.GetClaims(new User { Id = userId });
            var isAdmin = userClaims.Any(c => c.OperationClaimName == "Admin");

            if (!isAdmin)
            {
                return new ErrorResult(Messages.CheckIfUserIsAdmin);
            }

            return new SuccessResult();
        }

        private IResult CheckIfOldPasswordIsValid(User user, string oldPassword)
        {
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            bool isPasswordValid = HashingHelper.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt);
            if (!isPasswordValid)
            {
                return new ErrorResult(Messages.PasswordError);
            }

            return new SuccessResult();
        }
    }
}
