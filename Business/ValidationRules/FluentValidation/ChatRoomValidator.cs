using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ChatRoomValidator : AbstractValidator<ChatRoom>
    {
        public ChatRoomValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Messages.ChatRoomNameRequired)
                .MaximumLength(100).WithMessage(Messages.ChatRoomNameTooLong);

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Odanın oluşturulma tarihi gereklidir.");
        }
    }
}
