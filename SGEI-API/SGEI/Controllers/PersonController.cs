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
  public class PersonController : ControllerBase
  {
    private readonly IPersonRepository _personRepository;

    public PersonController(IPersonRepository personRepository)
    {
      _personRepository = personRepository;
    }

    [AllowAnonymous]
    [HttpGet("GetPersons")]
    public ActionResult<List<Person>> GetPersons()
    {
      List<Person> result = _personRepository.GetPersons();
      return new JsonResult(result);
    }
  }
}
