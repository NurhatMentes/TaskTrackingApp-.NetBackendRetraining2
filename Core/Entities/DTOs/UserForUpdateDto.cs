using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.DTOs
{
    public class UserForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }
        public bool? OnlineStatus { get; set; }
    }

}
