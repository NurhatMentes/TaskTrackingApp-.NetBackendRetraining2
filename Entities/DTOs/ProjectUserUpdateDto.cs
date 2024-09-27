using Core.Entities;

namespace Entities.DTOs
{
    public class ProjectUserUpdateDto : IDTOs
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int? NewUserId { get; set; }
        public string? Role { get; set; }
        public string? UpdatedByUserEmail { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public string? ProjectName { get; set; }
    }
}
