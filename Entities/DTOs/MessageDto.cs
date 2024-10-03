using Core.Entities;

namespace Entities.DTOs
{
    public class MessageDto : IDTOs
    {
        public int Id { get; set; }
        public string ChatRoomName { get; set; }
        public string MessageSenderName { get; set; }
        public string SenderMail { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}