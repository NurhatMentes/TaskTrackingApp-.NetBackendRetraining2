using Core.Entities;

namespace Entities.DTOs
{
    public class NotificationAddDto : IDTOs
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
