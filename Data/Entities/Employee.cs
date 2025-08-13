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
        public required int WorkExperience { get; set; } = 0;
        public DateTime DateOfBirth { get; set; }
        public required string Address { get; set; }
        public int YearsOld { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
