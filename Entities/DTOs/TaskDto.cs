using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class TaskDto : IDTOs
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int CreaterUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public int? AssignedUserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CreaterUserName { get; set; }
        public string? AssignedUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public PriorityLevel Priority { get; set; }
        public string? Status { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
