using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Task : IEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? AssignedUserId { get; set; }
        public int Priority { get; set; }
        public bool Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

 
        public Project? Project { get; set; }
        public User? AssignedUser { get; set; }
        public ICollection<TaskComment>? TaskComments { get; set; }
    }


}
