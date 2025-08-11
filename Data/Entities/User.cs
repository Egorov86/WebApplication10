namespace WebApplication10.Data.Entities
{
    public class User : Entity
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordHash { get; set; }
    }
}
