using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;
        private readonly IChatRoomService _chatRoomService;
        private readonly IUserService _userService;

        public MessageManager(IMessageDal messageDal, IChatRoomService chatRoomService, IUserService userService)
        {
            _messageDal = messageDal;
            _chatRoomService = chatRoomService;
            _userService = userService; 
        }

        [SecuredOperation("Admin,Project Manager,Member")]
        [ValidationAspect(typeof(MessageValidator))]
        public IResult Add(MessageAddDto messageAddDto)
        {
            var result = BusinessRules.Run(
            CheckIfChatRoomExists(messageAddDto.ChatRoomId),
            CheckIfUserExists(messageAddDto.MessageSenderId)
        );

            if (result != null)
            {
                return result; 
            }

            var message = new Message
            {
                ChatRoomId = messageAddDto.ChatRoomId,
                UserId = messageAddDto.MessageSenderId,
                Content = messageAddDto.Content,
                SentAt = DateTime.Now
            };

            _messageDal.Add(message);
            return new SuccessResult(Messages.MessageAdded);
        }

        [SecuredOperation("Admin,Project Manager,Member")]
        [ValidationAspect(typeof(MessageValidator))]
        public IResult Update(MessageUpdateDto messageUpdateDto)
        {
            var message = _messageDal.Get(m => m.Id == messageUpdateDto.Id);
            if (message == null)
            {
                return new ErrorResult(Messages.MessageUpdateNotFound);
            }

            var result = BusinessRules.Run(CheckIfMessageCanBeUpdated(message));

            if (result != null)
            {
                return result; 
            }

            message.Content = messageUpdateDto.Content;
            message.SentAt = DateTime.Now;

            _messageDal.Update(message);
            return new SuccessResult(Messages.MessageUpdated);
        }

        [SecuredOperation("Admin,Project Manager,Member")]
        [ValidationAspect(typeof(MessageValidator))]
        public IResult Delete(int id)
        {
            var messageToDelete = _messageDal.Get(m => m.Id == id);
            if (messageToDelete == null)
            {
                return new ErrorResult(Messages.MessageNotFound);
            }

            _messageDal.Delete(messageToDelete);
            return new SuccessResult(Messages.MessageDeleted);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<MessageDto> GetById(int id)
        {
            var result = _messageDal.GetMessagesByChatRoomId(id).Data.FirstOrDefault();
            if (result == null)
            {
                return new ErrorDataResult<MessageDto>(Messages.MessageNotFound);
            }

            return new SuccessDataResult<MessageDto>(result);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<MessageDto>> GetAll()
        {
            var result = _messageDal.GetAllMessages().Data;
            return new SuccessDataResult<List<MessageDto>>(result);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<MessageDto>> GetMessagesByChatRoomId(int chatRoomId)
        {
            var result = _messageDal.GetMessagesByChatRoomId(chatRoomId).Data;
            if (!result.Any())
            {
                return new ErrorDataResult<List<MessageDto>>(Messages.MessagesNotFoundInChatRoom);
            }

            return new SuccessDataResult<List<MessageDto>>(result);
        }


        //**************Business Rules**************
        private IResult CheckIfChatRoomExists(int chatRoomId)
        {
            var chatRoom = _chatRoomService.GetById(chatRoomId).Data;

            if (chatRoom == null)
            {
                return new ErrorResult(Messages.ChatRoomNotFound);
            }

            return new SuccessResult();
        }
        private IResult CheckIfUserExists(int userId)
        {
            var user = _userService.GetById(userId).Data;

            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            return new SuccessResult();
        }
        private IResult CheckIfMessageCanBeUpdated(Message message)
        {
            var timeSinceSent = DateTime.Now - message.SentAt;

            if (timeSinceSent.TotalMinutes > 5)
            {
                return new ErrorResult(Messages.MessageUpdateNotAllowed);
            }

            return new SuccessResult();
        }

    }
}
