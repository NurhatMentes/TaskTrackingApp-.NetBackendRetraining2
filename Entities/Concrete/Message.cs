using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; } 
        public ChatRoom ChatRoom { get; set; }

        public int UserId { get; set; } 
        public User User { get; set; }

        public string Content { get; set; } 
        public DateTime SendAt { get; set; } 
    }
}
