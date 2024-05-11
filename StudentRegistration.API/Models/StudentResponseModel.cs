using StudentRegistration.Domain.Models;

namespace StudentRegistration.API.Models
{
    public class StudentResponseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public Department Department { get; set; }

        public static StudentResponseModel fromStudent(Student student)
        {
            return new StudentResponseModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Department = student.Department,
            };
        }
    }
}
