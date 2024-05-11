using StudentRegistration.BusinessLogic.Interfaces;
using StudentRegistration.Data.Interfaces;
using StudentRegistration.Domain.Models;

namespace StudentRegistration.BusinessLogic
{
    public class StudentRegistrationManager : IStudentRegistrationManager
    {
        IStudentRegistrationRepository _studentRegistrationRepository;
        IPasswordManager _passwordManager;

        public StudentRegistrationManager(IStudentRegistrationRepository studentRegistrationRepository, IPasswordManager passwordManager)
        {
            _passwordManager = passwordManager;
            _studentRegistrationRepository = studentRegistrationRepository;
        }

        public Guid Register(Student student)
        {
            //To avoid storing the password in plain text, let's encrypt
            student.Password = _passwordManager.Encrypt(student.Password);

            var id = _studentRegistrationRepository.Save(student);

            return id;
        }

        public List<Student> GetAll()
        {
            return _studentRegistrationRepository.GetAll();
        }
    }
}
