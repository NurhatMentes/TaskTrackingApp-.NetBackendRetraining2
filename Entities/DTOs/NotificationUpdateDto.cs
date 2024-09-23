using Core.Entities;

namespace Entities.DTOs
{
    public class NotificationUpdateDto : IDTOs
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
    }
}
