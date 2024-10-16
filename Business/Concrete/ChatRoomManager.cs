using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class ChatRoomManager : IChatRoomService
    {
        private readonly IChatRoomDal _chatRoomDal;
        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _httpContextAccessor;
        private IChatRoomUserDal _chatRoomUserDal;
        private readonly IUserService _userService;

        public ChatRoomManager(IChatRoomDal chatRoomDal, ITokenHelper tokenHelper,
            IHttpContextAccessor httpContextAccessor, IChatRoomUserDal chatRoomUserDal, IUserService userService)
        {
            _chatRoomDal = chatRoomDal;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
            _chatRoomUserDal = chatRoomUserDal;
            _userService = userService;
        }

        [ValidationAspect(typeof(ChatRoomAddValidator))]
        public IResult Add(ChatRoomAddDto chatRoomAddDto)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var createdByUserId = _tokenHelper.GetUserIdFromToken(token);

            var chatRoom = new ChatRoom
            {
                Name = chatRoomAddDto.Name,
                CreatedByUserId = createdByUserId,
                CreatedAt = chatRoomAddDto.CreatedAt
            };

            _chatRoomDal.Add(chatRoom);


            foreach (var userId in chatRoomAddDto.UserIds)
            {
                var chatRoomUser = new ChatRoomUser
                {
                    ChatRoomId = chatRoom.Id,
                    UserId = userId     
                };
                _chatRoomUserDal.Add(chatRoomUser);  
            }

            return new SuccessResult(Messages.ChatRoomCreated);
        }

        [ValidationAspect(typeof(ChatRoomUpdateValidator))]
        public IResult Update(ChatRoomUpdateDto chatRoomUpdateDto)
        {
            var existingChatRoom = _chatRoomDal.Get(c => c.Id == chatRoomUpdateDto.Id);
            if (existingChatRoom == null)
            {
                return new ErrorResult(Messages.ChatRoomNotFound);
            }

            existingChatRoom.Name = chatRoomUpdateDto.Name;
            existingChatRoom.UpdatedAt = chatRoomUpdateDto.UpdatedAt;

            _chatRoomDal.Update(existingChatRoom);
            return new SuccessResult(Messages.ChatRoomUpdated);
        }

        public IResult Delete(int chatRoomId)
        {
            var chatRoomToDelete = _chatRoomDal.Get(c => c.Id == chatRoomId);
            if (chatRoomToDelete == null)
            {
                return new ErrorResult(Messages.ChatRoomNotFound);
            }

            _chatRoomDal.Delete(chatRoomToDelete);
            return new SuccessResult(Messages.ChatRoomDeleted);
        }

        public IDataResult<ChatRoomDetailDto> GetById(int chatRoomId)
        {
            var chatRoom = _chatRoomDal.Get(c => c.Id == chatRoomId);
            if (chatRoom == null)
            {
                return new ErrorDataResult<ChatRoomDetailDto>(Messages.ChatRoomNotFound);
            }

            var chatRoomDetailDto = new ChatRoomDetailDto
            {
                Id = chatRoom.Id,
                Name = chatRoom.Name,
                CreatedAt = chatRoom.CreatedAt
            };

            return new SuccessDataResult<ChatRoomDetailDto>(chatRoomDetailDto);
        }

        //[CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<ChatRoomDetailDto>> GetAll()
        {
            var chatRooms = _chatRoomDal.GetAll();
            return new SuccessDataResult<List<ChatRoomDetailDto>>(chatRooms.Data);
        }

        public IDataResult<List<ChatRoomDetailDto>> GetChatRooms()
        {
            var userId = _tokenHelper.GetUserIdFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            var user = _userService.GetById(userId).Data;
            var userClaims = _userService.GetClaims(user);
            bool isAdmin = userClaims.Data.Any(claim => claim.OperationClaimName == "Admin");

            if (isAdmin)
            {  
                var allChatRooms = _chatRoomDal.GetAllChatRoomsWithProjectsAndUsers();
                return new SuccessDataResult<List<ChatRoomDetailDto>>(allChatRooms);
            }
            else
            {
                var userChatRooms = _chatRoomDal.GetChatRoomsWithProjectsAndUsersByUserId(userId);
                return new SuccessDataResult<List<ChatRoomDetailDto>>(userChatRooms);
            }
        }

    }

}
