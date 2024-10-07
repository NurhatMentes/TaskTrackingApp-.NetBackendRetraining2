using Core.Entities;

namespace Entities.DTOs
{
    public class ProjectUserDto :IDTOs
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }
        public string ProjectName { get; set; } 
        public string UserName { get; set; } 
        public string UserEmail { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? UpdatedByUserEmail { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
