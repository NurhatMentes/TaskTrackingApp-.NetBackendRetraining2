using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Project : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

      
        public User? CreatedByUser { get; set; }
        public ICollection<ProjectUser>? ProjectUsers { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
