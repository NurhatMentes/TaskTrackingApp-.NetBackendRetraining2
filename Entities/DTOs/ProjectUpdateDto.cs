using Core.Entities;

namespace Entities.DTOs
{
    public class ProjectUpdateDto : IDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public string? UpdatedByUserEmail { get; set; }
        public string? UpdatedByUserName { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
