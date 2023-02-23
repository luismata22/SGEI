using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGEI.Interfaces;
using SGEI.Models.Master;
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
  }
}
