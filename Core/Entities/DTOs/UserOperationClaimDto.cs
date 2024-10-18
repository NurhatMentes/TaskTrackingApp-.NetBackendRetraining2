namespace Core.Entities.DTOs
{
    public class UserOperationClaimDto : IDTOs
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OperationClaimName { get; set; }
    }
}
