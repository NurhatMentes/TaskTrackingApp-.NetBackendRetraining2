using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Notification : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public User? User { get; set; }
    }


}
