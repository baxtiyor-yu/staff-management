

namespace Synel_staff.Domain.Entities
{
    public class Employee
    {
        
        public int Id { get; set; }

        public required string PayrollNumber { get; set; }

        public required string Forenames { get; set; }

        public required string Surname { get; set; }

        public DateOnly DateOfBirth { get; set; } = default(DateOnly);

        public string? Telephone { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }

        public string? Address2 { get; set; }

        public string? Postcode { get; set; }

        public string? EMailHome { get; set; }

        public DateOnly StartDate { get; set; } = default(DateOnly);
    }
}
