using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class TaskAddDto : IDTOs
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CreaterUserId { get; set; }
        public int? AssignedUserId { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool? Status { get; set; }
        public DateTime EndDate { get; set; }
    }
}
