using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    [SecuredOperation("Admin,Project Manager,Member")]
    [ValidationAspect(typeof(ChatRoomValidator))]
    public class ChatRoomManager : IChatRoomService
    {
        private readonly IChatRoomDal _chatRoomDal;

        public ChatRoomManager(IChatRoomDal chatRoomDal)
        {
            _chatRoomDal = chatRoomDal;
        }

        public IResult Add(ChatRoomCreateDto chatRoomCreateDto)
        {
            var chatRoom = new ChatRoom
            {
                Name = chatRoomCreateDto.Name,
                CreatedAt = chatRoomCreateDto.CreatedAt
            };

            _chatRoomDal.Add(chatRoom);
            return new SuccessResult(Messages.ChatRoomCreated);
        }

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

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<ChatRoomDetailDto>> GetAll()
        {
            var chatRooms = _chatRoomDal.GetAll();
            var chatRoomDetails = chatRooms.Select(c => new ChatRoomDetailDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatedAt = c.CreatedAt
            }).ToList();

            return new SuccessDataResult<List<ChatRoomDetailDto>>(chatRoomDetails);
        }
    }

}
