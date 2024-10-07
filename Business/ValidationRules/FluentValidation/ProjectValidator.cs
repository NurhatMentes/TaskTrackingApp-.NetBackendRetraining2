    using Business.Constants;
    using Entities.Concrete;
    using FluentValidation;

    namespace Business.ValidationRules.FluentValidation
    {
        public class ProjectValidator : AbstractValidator<Project>
        {
            public ProjectValidator()
            {
                RuleFor(project => project.Name)
                    .NotEmpty().WithMessage(Messages.ProjectNameNotEmpty)
                    .MinimumLength(3).WithMessage(Messages.ProjectNameMinLength);

                RuleFor(project => project.Description)
                    .MaximumLength(1000).WithMessage(Messages.ProjectDescriptionMaxLength);

           
                RuleFor(project => project.StartDate)
                    .NotEmpty().WithMessage(Messages.ProjectStartDateNotEmpty)
                    .GreaterThanOrEqualTo(DateTime.Now).WithMessage(Messages.ProjectStartDateGreaterThanNow);

                RuleFor(project => project.EndDate)
                    .GreaterThanOrEqualTo(project => project.StartDate)
                    .WithMessage(Messages.ProjectEndDateGreaterThanStartDate);

                RuleFor(project => project.Status)
                    .NotEmpty().WithMessage(Messages.ProjectStatusNotEmpty)
                    .IsInEnum().WithMessage(Messages.ProjectStatusInvalid);

                RuleFor(project => project.CreatedByUserId)
                    .NotEmpty().WithMessage(Messages.CreatedByUserIdNotEmpty);
            }
        }
    }
