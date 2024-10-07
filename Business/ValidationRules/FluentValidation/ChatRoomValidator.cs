using Business.Constants;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ChatRoomAddValidator : AbstractValidator<ChatRoomAddDto>
    {
        public ChatRoomAddValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Messages.ChatRoomNameRequired)
                .MaximumLength(100).WithMessage(Messages.ChatRoomNameTooLong);

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Odanın oluşturulma tarihi gereklidir.");
        }
    }

    public class ChatRoomUpdateValidator : AbstractValidator<ChatRoomUpdateDto>
    {
        public ChatRoomUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Messages.ChatRoomNameRequired)
                .MaximumLength(100).WithMessage(Messages.ChatRoomNameTooLong);

            RuleFor(x => x.UpdatedAt)
                .NotEmpty().WithMessage("Odanın güncellenme tarihi gereklidir.");
        }
    }
}
