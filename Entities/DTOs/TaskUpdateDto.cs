using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class TaskUpdateDto : IDTOs
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int? AssignedUserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool? Status { get; set; }
        public DateTime EndDate { get; set; }
        public string? UpdatedByUserEmail { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
}
