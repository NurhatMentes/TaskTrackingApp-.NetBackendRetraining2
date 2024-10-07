using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProjectUserAddValidator : AbstractValidator<ProjectUserAddDto>
    {
        public ProjectUserAddValidator()
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
    public class ProjectUserUpdateValidator : AbstractValidator<ProjectUserUpdateDto>
    {
        public ProjectUserUpdateValidator()
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
