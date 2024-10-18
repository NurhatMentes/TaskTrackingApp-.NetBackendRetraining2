using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult UserAdd(User user);
        IDataResult<UserForRegisterDto> Add(UserForRegisterDto userForRegisterDto, string password);
        IResult Update(UserForUpdateDto userForUpdateDto, int userId);
        IResult AdminUserUpdate(UserForAdminUpdateDto userForAdminUpdateDto);
        IResult UpdatePassword(int userId, string currentPassword, string newPassword);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int userId);
        IDataResult<UserDetailDto> GetUserDetail(int userId);
    }
}
