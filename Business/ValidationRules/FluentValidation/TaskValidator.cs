using Business.Constants;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TaskAddValidator : AbstractValidator<TaskAddDto>
    {
        public TaskAddValidator()
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

    public class TaskUpdateValidator : AbstractValidator<TaskUpdateDto>
    {
        public TaskUpdateValidator()
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
