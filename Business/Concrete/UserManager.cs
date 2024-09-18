using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _dal;

        public UserManager(IUserDal dal)
        {
            _dal = dal;
        }

        public IResult Add(User user)
        {
            _dal.Add(user);
            return new SuccessResult(Messages.UserRegistered);
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_dal.Get(p=>p.Email==email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_dal.GetClaims(user));          
        }
    }
}
