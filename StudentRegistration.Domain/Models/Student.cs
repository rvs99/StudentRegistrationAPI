namespace StudentRegistration.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public Department Department { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
