using Business.Constants;
using Entities.DTOs;
using FluentValidation;

    namespace Business.ValidationRules.FluentValidation
    {
        public class ProjectAddValidator : AbstractValidator<ProjectAddDto>
        {
            public ProjectAddValidator()
            {
                RuleFor(project => project.ProjectName)
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

            }
        }
    public class ProjectUpdateValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateValidator()
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

        }
    }
}
