using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class ProjectUser : IEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }

  
        public Project? Project { get; set; }
        public User? User { get; set; }
    }
}
