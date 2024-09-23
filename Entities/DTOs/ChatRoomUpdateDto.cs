using Core.Entities;

namespace Entities.DTOs
{
    public class ChatRoomUpdateDto : IDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

}
