using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IChatRoomUserService
    {
        IResult Add(ChatRoomUserAddDto chatRoomUserAddDto);
        IResult Delete(int chatRoomId, int userId);
        IDataResult<List<ChatRoomUserDto>> GetAll();
        IDataResult<ChatRoomUser> GetByChatRoomIdWithUserId(int chatRoomId, int userId);
        IDataResult<List<ChatRoomUserDto>> GetByChatRoomId(int chatRoomId);
        IDataResult<List<ChatRoomUserDto>> GetByWithUserId(int userId);
        IResult JoinChatRoom(int chatRoomId);
    }

}
