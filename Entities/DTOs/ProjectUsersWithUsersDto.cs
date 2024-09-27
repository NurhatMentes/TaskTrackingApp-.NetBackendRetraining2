using Core.Entities;


namespace Entities.DTOs
{
    public class ProjectUsersWithUsersDto : IDTOs
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Role { get; set; }
    }
}
