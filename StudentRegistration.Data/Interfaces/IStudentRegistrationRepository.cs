using StudentRegistration.Domain.Models;

namespace StudentRegistration.Data.Interfaces
{
    public interface IStudentRegistrationRepository
    {
        public Guid Save(Student student);

        public List<Student> GetAll();
    }
}
