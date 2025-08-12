namespace WebApplication10.Data.Entities
{
    public class Employee : Entity
    {
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string MiddleName { get; set; }
        public required string Education { get; set; }
        public required string Profession { get; set; }
        public required string Email { get; set; }
    }
}
