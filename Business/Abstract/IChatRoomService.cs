using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IChatRoomService
    {
        IResult Add(ChatRoomAddDto chatRoomCreateDto);
        IResult Update(ChatRoomUpdateDto chatRoomUpdateDto);
        IResult Delete(int chatRoomId);
        IDataResult<ChatRoomDetailDto> GetById(int chatRoomId);
        IDataResult<List<ChatRoomDetailDto>> GetAll();
        IDataResult<List<ChatRoomDetailDto>> GetChatRooms();
    }
}
