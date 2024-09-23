using Core.Entities;

namespace Entities.DTOs
{
    public class ChatRoomUserDto : IDTOs
    {
        public string ChatRoomName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }

}
