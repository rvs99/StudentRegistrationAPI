using StudentRegistration.Domain.Models;

namespace StudentRegistration.Models
{
    public class StudentRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public Department Department { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Student toStudent()
        {
            return new Student()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Department = this.Department,
                Username = this.Username,
                Password = this.Password
            };
        }
    }
}
