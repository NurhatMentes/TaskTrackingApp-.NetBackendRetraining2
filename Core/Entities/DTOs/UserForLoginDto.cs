
namespace Core.Entities.DTOs
{
    public class UserForLoginDto : IDTOs
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
