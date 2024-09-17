using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class TaskComment : IEntity
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Comment { get; set; }
        public int CommentedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }

 
        public Task? Task { get; set; }
        public User? CommentedByUser { get; set; }
    }


}
