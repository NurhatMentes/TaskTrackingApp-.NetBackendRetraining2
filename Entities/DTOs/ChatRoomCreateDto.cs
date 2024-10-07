using Core.Entities;

namespace Entities.DTOs
{
    public class ChatRoomAddDto : IDTOs
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<int> UserIds { get; set; }
    }

}
