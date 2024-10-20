using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;

public class OperationClaimValidator : AbstractValidator<OperationClaim>
{
    private readonly IOperationClaimDal _operationClaimDal;

    public OperationClaimValidator(IOperationClaimDal operationClaimDal)
    {
        _operationClaimDal = operationClaimDal;

        RuleFor(c => c.OperationClaimName)
            .NotEmpty().WithMessage("Claim adı boş olamaz.")
            .Must(BeUniqueClaimName).WithMessage(Messages.OperationClaimAlreadyExists);

    }

    private bool BeUniqueClaimName(string claimName)
    {
        return _operationClaimDal.Get(c => c.OperationClaimName == claimName) == null;
    }
}
