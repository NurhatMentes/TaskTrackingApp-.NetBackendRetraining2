using Core.Entities;

namespace Entities.DTOs
{
    public class ProjectUserUpdateDto : IDTOs
    {
        public int ProjectId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }
    }
}
