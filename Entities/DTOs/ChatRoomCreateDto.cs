using Core.Entities;

namespace Entities.DTOs
{
    public class ChatRoomCreateDto : IDTOs
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
