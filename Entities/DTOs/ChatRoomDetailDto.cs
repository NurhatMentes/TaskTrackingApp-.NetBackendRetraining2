using Core.Entities;

namespace Entities.DTOs
{
    public class ChatRoomDetailDto : IDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? RelatedProjectName { get; set; }
        public string? CreatedByUserEmail { get; set; }
        public string? CreatedByUserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
