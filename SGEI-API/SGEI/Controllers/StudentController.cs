using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGEI.Interfaces;
using SGEI.Models.Master;
using SGEI.Models.Master.Filters;
using SGEI.Models.Security.Filters;
using System.Collections.Generic;

namespace SGEI.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class StudentController : ControllerBase
  {
    private readonly IStudentRepository _studentRepository;

    public StudentController(IStudentRepository studentRepository)
    {
      _studentRepository = studentRepository;
    }

    [AllowAnonymous]
    [HttpGet("GetTypeCourse")]
    public ActionResult<List<TypeCourse>> GetTypeCourse()
    {
      List<TypeCourse> result = _studentRepository.GetTypeCourse();
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult<List<Student>> Get([FromQuery] StudentFilters filter)
    {
      List<Student> result = _studentRepository.GetStudents(filter);
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetStudentById")]
    public ActionResult<Student> GetStudentById(long idStudent)
    {
      Student result = _studentRepository.GetStudentById(idStudent);
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<long> Post(Student model)
    {
      long result = _studentRepository.Post(model);
      return new JsonResult(result);
    }
  }
}
