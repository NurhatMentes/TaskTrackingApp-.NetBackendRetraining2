using Core.Entities;

namespace Entities.DTOs
{
    public class ChatRoomUserDto : IDTOs
    {
        public int ChatRoomId { get; set; }  
        public int UserId { get; set; }
        public string ChatRoomName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool OnlineStatus { get; set; }
    }

}
