using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class MessageAddValidator : AbstractValidator<MessageAddDto>
    {
        public MessageAddValidator()
        {
            RuleFor(message => message.Content)
                .NotEmpty().WithMessage(Messages.MessageContentCannotBeEmpty)
                .Length(1, 1000).WithMessage(Messages.MessageContentLength);

            RuleFor(message => message.ChatRoomId)
                .GreaterThan(0).WithMessage(Messages.InvalidChatRoom);

            RuleFor(message => message.MessageSenderId)
                .GreaterThan(0).WithMessage(Messages.InvalidUser);
        }
    }
    public class MessageUdpadteValidator : AbstractValidator<MessageUpdateDto>
    {
        public MessageUdpadteValidator()
        {
            RuleFor(message => message.Content)
                .NotEmpty().WithMessage(Messages.MessageContentCannotBeEmpty)
                .Length(1, 1000).WithMessage(Messages.MessageContentLength);

            RuleFor(message => message.Id)
                .GreaterThan(0).WithMessage(Messages.InvalidChatRoom);
        }
    }
}
