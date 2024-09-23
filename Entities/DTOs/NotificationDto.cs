using Core.Entities;

namespace Entities.DTOs
{
    public class NotificationDto : IDTOs
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
