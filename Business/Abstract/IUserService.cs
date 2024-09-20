using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult UserAdd(User user);
        IDataResult<UserForRegisterDto> Add(UserForRegisterDto userForRegisterDto, string password);
        IResult Update(UserForUpdateDto userForUpdateDto, int userId);
        IResult UpdatePassword(int userId, string currentPassword, string newPassword);
        IResult UpdateUserRole(int userId, int operationClaimId);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int userId);
        IDataResult<UserDetailDto> GetUserDetail(int userId);
    }
}
