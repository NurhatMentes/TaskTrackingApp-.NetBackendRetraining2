using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class ChatRoom : IEntity
    {
        public int Id { get; set; }
        public int CreatedByUserId { get; set; }
        public int? RelatedProjectId { get; set; }
        public string Name { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User? CreatedByUser { get; set; }
        public Project? RelatedProject { get; set; }
        public ICollection<ChatRoomUser> ChatRoomUsers { get; set; } 
        public ICollection<Message> Messages { get; set; } 
    }
}
