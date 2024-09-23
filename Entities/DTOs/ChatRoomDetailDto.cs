using Core.Entities;

namespace Entities.DTOs
{
    public class ChatRoomDetailDto : IDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
