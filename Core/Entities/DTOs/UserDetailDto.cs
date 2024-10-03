using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.DTOs
{
    public class UserDetailDto : IDTOs
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? ProjectName { get; set; } 
        public DateTime? ProjectStartDate { get; set; } 
        public DateTime? ProjectEndDate { get; set; } 
        public string? TaskName { get; set; } 
        public string? AssignedBy { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }
        public bool? OnlineStatus { get; set; }
    }

}
