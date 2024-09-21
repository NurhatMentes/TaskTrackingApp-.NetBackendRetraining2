using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProjectUserValidator : AbstractValidator<ProjectUser>
    {
        public ProjectUserValidator()
        {
            RuleFor(pu => pu.ProjectId)
                .NotEmpty().WithMessage(Messages.ProjectIdCannotBeEmpty)
                .GreaterThan(0).WithMessage(Messages.ProjectIdMustBeGreaterThanZero);

            RuleFor(pu => pu.UserId)
                .NotEmpty().WithMessage(Messages.UserIdCannotBeEmpty)
                .GreaterThan(0).WithMessage(Messages.UserIdMustBeGreaterThanZero);

            RuleFor(pu => pu.Role)
                .NotEmpty().WithMessage(Messages.RoleCannotBeEmpty)
                .Length(1, 100).WithMessage(Messages.RoleLengthMustBeBetween1And100);
        }
    }

}
