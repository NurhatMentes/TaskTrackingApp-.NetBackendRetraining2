using Business.Constants;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ChatRoomUserValidator : AbstractValidator<ChatRoomUserAddDto>
    {
        public ChatRoomUserValidator()
        {
            RuleFor(x => x.ChatRoomId)
            .NotEmpty().WithMessage(Messages.InvalidChatRoomId)
            .GreaterThan(0).WithMessage(Messages.InvalidChatRoomId);

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(Messages.InvalidUserId)
                .GreaterThan(0).WithMessage(Messages.InvalidUserId);
        }
    }
}
