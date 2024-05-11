using StudentRegistration.Domain.Models;

namespace StudentRegistration.BusinessLogic.Interfaces
{
    public interface IStudentRegistrationManager
    {
        public Guid Register(Student student);

        public List<Student> GetAll();

    }
}
