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

    [SecuredOperation("Admin,Project Manager,Member")]
    [ValidationAspect(typeof(ChatRoomUserValidator))]
    public class ChatRoomUserManager : IChatRoomUserService
    {
        private readonly IChatRoomUserDal _chatRoomUserDal;
        private readonly IUserService _userService;
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomUserManager(IChatRoomUserDal chatRoomUserDal, IUserService userService, IChatRoomService chatRoomService)
        {
            _chatRoomUserDal = chatRoomUserDal;
            _userService = userService;
            _chatRoomService = chatRoomService;
        }

        public IResult Add(ChatRoomUserAddDto chatRoomUserAddDto)
        {
            var result = BusinessRules.Run(
                CheckIfChatRoomExists(chatRoomUserAddDto.ChatRoomId),
                CheckIfUserExists(chatRoomUserAddDto.UserId),
                CheckIfChatRoomUserExists(chatRoomUserAddDto.ChatRoomId,chatRoomUserAddDto.UserId)
               );

            if (result != null)
            {
                return result;
            }

            var chatRoomUser = new ChatRoomUser
            {
                ChatRoomId = chatRoomUserAddDto.ChatRoomId,
                UserId = chatRoomUserAddDto.UserId
            };

            _chatRoomUserDal.Add(chatRoomUser);
            return new SuccessResult(Messages.ChatRoomUserAdded);
        }

        public IResult Delete(int chatRoomId, int userId)
        {
            var chatRoomUser = _chatRoomUserDal.Get(cru => cru.ChatRoomId == chatRoomId && cru.UserId == userId);
            if (chatRoomUser == null)
            {
                return new ErrorResult(Messages.ChatRoomUserNotFound);
            }

            _chatRoomUserDal.Delete(chatRoomUser);
            return new SuccessResult(Messages.ChatRoomUserDeleted);
        }

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<ChatRoomUserDto>> GetAll()
        {
            var result = _chatRoomUserDal.GetAllChatRoomsWithUsers();
            return new SuccessDataResult<List<ChatRoomUserDto>>(result.Data, Messages.ChatRoomUsersListed);
        }

        public IDataResult<ChatRoomUser> GetByChatRoomIdWithUserId(int chatRoomId, int userId)
        {
            var result = _chatRoomUserDal.Get(cru => cru.ChatRoomId == chatRoomId && cru.UserId == userId);
            if (result == null)
            {
                return new ErrorDataResult<ChatRoomUser>(Messages.ChatRoomUserNotFound);
            }

            return new SuccessDataResult<ChatRoomUser>(result, Messages.ChatRoomUserFound);
        }

        public IDataResult<List<ChatRoomUserDto>> GetByChatRoomId(int chatRoomId)
        {
            var result = _chatRoomUserDal.GetAllChatRoomsWithUsersByChatRoomId(chatRoomId).Data;

            if (result == null)
            {
                return new ErrorDataResult<List<ChatRoomUserDto>>(Messages.ChatRoomNotFound);
            }

            return new SuccessDataResult<List<ChatRoomUserDto>>(result, Messages.ChatRoomUsersListed);
        }

        public IDataResult<List<ChatRoomUserDto>> GetByWithUserId(int userId)
        {
            var result = _chatRoomUserDal.GetAllChatRoomsWithUsersByUserId(userId).Data;

            if (result == null)
            {
                return new ErrorDataResult<List<ChatRoomUserDto>>(Messages.ChatRoomUserNotFound);
            }

            return new SuccessDataResult<List<ChatRoomUserDto>>(result, Messages.ChatRoomUserFound);
        }



        //**************Business Rules**************
        private IResult CheckIfUserExists(int userId)
        {
            var userExists = _userService.GetById(userId) != null;
            if (!userExists)
            {
                return new ErrorResult(Messages.UserNotFound); 
            }
            return new SuccessResult();
        }

        private IResult CheckIfChatRoomUserExists(int chatRoomId, int userId)
        {
            var chatRoomUserExists = _chatRoomUserDal.Get(c => c.ChatRoomId == chatRoomId && c.UserId == userId);

            if (chatRoomUserExists != null)
            {
                return new ErrorResult(Messages.ChatRoomUserAlreadyExists);
            }

            return new SuccessResult();
        }


        private IResult CheckIfChatRoomExists(int chatRoomId)
        {
            var result = _chatRoomService.GetById(chatRoomId);

            if (result == null || !result.IsSuccess) 
            {
                return new ErrorResult(Messages.ChatRoomNotFound);
            }

            return new SuccessResult();
        }
    }
}
