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
        public int CreaterUserId { get; set; }
        public int? AssignedUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool? Status { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        
        public Project? Project { get; set; }
        public User? AssignedUser { get; set; }
    }
    public enum PriorityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
