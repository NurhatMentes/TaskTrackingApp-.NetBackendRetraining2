using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IOperationClaimDal _operationClaimDal;
        private IUserOperationClaimDal _userOperationClaimDal;
        private IUserDal _userDal;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper,
                         IOperationClaimDal operationClaimDal, IUserOperationClaimDal userOperationClaimDal, IUserDal userDal, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _operationClaimDal = operationClaimDal;
            _userOperationClaimDal = userOperationClaimDal;
            _userDal = userDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
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
                Status = false
            };

            _userService.UserAdd(user);

            var memberRole = _operationClaimDal.Get(c => c.OperationClaimName == "Member");
            if (memberRole != null)
            {
                var userOperationClaim = new UserOperationClaim
                {
                    UserId = user.Id,
                    OperationClaimId = memberRole.Id
                };
                _userOperationClaimDal.Add(userOperationClaim);
            }

            var userForRegisterDtoResponse = new UserForRegisterDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt
            };

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }



        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            userToCheck.OnlineStatus = true;
            _userDal.Update(userToCheck);
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult Logout()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            TokenBlacklist.AddToken(token);

            var userId = _tokenHelper.GetUserIdFromToken(token);

            var existingUser = _userDal.Get(p => p.Id == userId);
            if (existingUser != null)
            {
                existingUser.OnlineStatus = false;
                _userDal.Update(existingUser);
            }

            return new SuccessResult(Messages.UserLoggedOut);
        }



        public IResult UserExists(string email)
        {
            var user = _userService.GetByMail(email).Data; 
            if (user != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }


        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
