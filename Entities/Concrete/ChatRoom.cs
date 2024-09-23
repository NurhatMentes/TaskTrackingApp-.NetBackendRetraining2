using Core.Entities;

namespace Entities.Concrete
{
    public class ChatRoom : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public DateTime CreatedAt { get; set; }
        public ICollection<ChatRoomUser> ChatRoomUsers { get; set; } 
        public ICollection<Message> Messages { get; set; } 
    }
}
