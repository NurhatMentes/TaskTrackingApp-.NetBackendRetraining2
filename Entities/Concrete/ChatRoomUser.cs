using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class ChatRoomUser : IEntity
    {
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

}
