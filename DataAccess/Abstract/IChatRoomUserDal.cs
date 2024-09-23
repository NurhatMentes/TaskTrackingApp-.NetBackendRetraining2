using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;


namespace DataAccess.Abstract
{
    public interface IChatRoomUserDal : IEntityRepository<ChatRoomUser>
    {
        DataResult<List<ChatRoomUserDto>> GetAllChatRoomsWithUsers();
        DataResult<List<ChatRoomUserDto>> GetAllChatRoomsWithUsersByUserId(int userId);
        DataResult<List<ChatRoomUserDto>> GetAllChatRoomsWithUsersByChatRoomId(int chatRoomId);
    }
}
