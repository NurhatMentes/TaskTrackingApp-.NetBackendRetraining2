using Core.Entities;
using Entities.Concrete;
using Task = Entities.Concrete.Task;

namespace Entities.DTOs
{
    public class ProjectUserTaskDto : IDTOs
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public bool ProjectStatus { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string UpdatedByUserName { get; set; }
        public string UpdatedByUserEmail { get; set; }
        public DateTime? UpdatedAt { get; set; }

      
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
