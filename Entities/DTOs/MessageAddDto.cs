using Core.Entities;

namespace Entities.DTOs
{
    public class MessageAddDto : IDTOs
    {
        public int ChatRoomId { get; set; } 
        public int MessageSenderId { get; set; } 
        public string Content { get; set; }
    }

}
