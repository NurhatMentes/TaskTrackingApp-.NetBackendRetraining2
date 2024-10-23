using Business.Constants;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimValidator() : this(null)
        {
        }

        public UserOperationClaimValidator(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;

            RuleFor(u => u.UserId)
                .NotEmpty()
                .WithMessage(Messages.UserIdIsRequired);

            RuleFor(u => u.OperationClaimId)
                .NotEmpty()
                .WithMessage(Messages.OperationClaimIdIsRequired);

            RuleFor(u => u)
                .Must(NotHaveDuplicateOperationClaim)
                .WithMessage(Messages.UserAlreadyHasThisClaim);
        }

        private bool NotHaveDuplicateOperationClaim(UserOperationClaim userOperationClaim)
        {
            if (_userOperationClaimDal == null)
                return true;
            var existingClaim = _userOperationClaimDal.Get(u =>
                u.UserId == userOperationClaim.UserId &&
                u.OperationClaimId == userOperationClaim.OperationClaimId);

            return existingClaim == null; 
        }
    }
}
