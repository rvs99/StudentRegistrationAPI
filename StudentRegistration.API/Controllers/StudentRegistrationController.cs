using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentRegistration.API.Models;
using StudentRegistration.BusinessLogic.Interfaces;
using StudentRegistration.Models;
using System.Net;

namespace StudentRegistration.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("[controller]")]
    public class StudentRegistrationController : ControllerBase
    {
        private IStudentRegistrationManager _studentRegistration;

        public StudentRegistrationController(IStudentRegistrationManager studentRegistration)
        {
            _studentRegistration = studentRegistration;
        }

        [HttpPost(Name = "Register")]
        public IActionResult RegisterStudent(StudentRequestModel studentToBeRegister)
        {
            var id = _studentRegistration.Register(studentToBeRegister.toStudent());

            return Ok(id);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var returnList = new List<StudentResponseModel>();
                _studentRegistration.GetAll().ForEach(s => returnList.Add(StudentResponseModel.fromStudent(s)));

                return Ok(returnList);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}