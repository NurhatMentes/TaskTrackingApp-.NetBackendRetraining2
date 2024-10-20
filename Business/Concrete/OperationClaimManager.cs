using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    [SecuredOperation("Admin")]
    public class OperationClaimManager : IOperationClaimService
    {
        public IOperationClaimDal _dal;

        public OperationClaimManager(IOperationClaimDal dal)
        {
           _dal = dal;
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            var claim = new OperationClaim
            {   
                OperationClaimName = operationClaim.OperationClaimName
            };

            _dal.Add(claim);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var claim = _dal.Get(c=>c.Id == id);
            if (claim == null) {return new ErrorResult(Messages.OperationClaimNotFound);}

            _dal.Delete(claim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        public IDataResult<List<OperationClaim>> GetClaims()
        {
            return new SuccessDataResult<List<OperationClaim>>(_dal.GetAll());    
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            var claim = _dal.Get(c => c.Id == operationClaim.Id);

            if (claim == null) { return new ErrorResult(Messages.OperationClaimNotFound); }

            var newClaim = new OperationClaim
            {
                Id = operationClaim.Id,
                OperationClaimName = operationClaim.OperationClaimName
            };
            _dal.Update(newClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }
    }
}
