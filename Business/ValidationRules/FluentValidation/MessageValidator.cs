using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Content)
                .NotEmpty().WithMessage(Messages.MessageContentCannotBeEmpty)
                .Length(1, 1000).WithMessage(Messages.MessageContentLength);

            RuleFor(message => message.ChatRoomId)
                .GreaterThan(0).WithMessage(Messages.InvalidChatRoom);

            RuleFor(message => message.UserId)
                .GreaterThan(0).WithMessage(Messages.InvalidUser);
        }
    }
}
