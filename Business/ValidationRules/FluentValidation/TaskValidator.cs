using Business.Constants;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TaskValidator : AbstractValidator<TaskAddDto>
    {
        public TaskValidator()
        {
            RuleFor(t => t.Name)
            .NotEmpty().WithMessage(Messages.TaskNameRequired);

            RuleFor(t => t.Description)
                .MaximumLength(1000).WithMessage(Messages.TaskDescriptionMaxLength);

            RuleFor(t => t.EndDate)
                .NotEmpty().WithMessage(Messages.TaskEndDateRequired)
                .GreaterThan(DateTime.Now).WithMessage(Messages.TaskEndDateInFuture);

            RuleFor(t => t.Priority)
            .IsInEnum().WithMessage(Messages.TaskPriorityInvalid);

            RuleFor(t => t.Status)
                .NotNull().WithMessage(Messages.TaskStatusRequired);
        }
    }

}
