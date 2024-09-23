using Core.Entities;

namespace Entities.DTOs
{
    public class ChatRoomUserAddDto : IDTOs
    {
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
    }

}
