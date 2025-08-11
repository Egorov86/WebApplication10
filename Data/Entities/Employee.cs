﻿namespace WebApplication10.Data.Entities
{
    public class Employee : Entity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}
