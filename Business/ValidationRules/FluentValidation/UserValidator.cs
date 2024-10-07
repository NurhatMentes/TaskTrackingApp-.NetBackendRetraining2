using Core.Entities.Concrete;
using Core.Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserAddValidator : AbstractValidator<User>
    {
        public UserAddValidator()
        {
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();

            RuleFor(p => p.FirstName).MinimumLength(2);
            RuleFor(p => p.LastName).MinimumLength(2);
            RuleFor(p => p.Email).EmailAddress();
        }
    }
    public class UserUdpateValidator : AbstractValidator<UserForUpdateDto>
    {
        public UserUdpateValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();

            RuleFor(p => p.FirstName).MinimumLength(2);
            RuleFor(p => p.LastName).MinimumLength(2);
            RuleFor(p => p.Status).NotEmpty();
        }
    }
}
