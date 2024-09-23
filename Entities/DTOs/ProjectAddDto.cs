using Core.Entities;

namespace Entities.DTOs
{
    public class ProjectAddDto : IDTOs
    {
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
