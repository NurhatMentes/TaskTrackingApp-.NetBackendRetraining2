using Core.Entities;

namespace Entities.DTOs
{
    public class ProjectUserAddDto :IDTOs
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; } 
    }

}
