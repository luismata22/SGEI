using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGEI.Interfaces;
using SGEI.Models;
using SGEI.Models.Security.Filters;
using SGEI.Utils;
using System.Collections.Generic;

namespace SGEI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpGet("GetUsers")]
    public ActionResult<List<User>> GetUsers([FromQuery] UserFilters filter)
    {
      List<User> result = _userRepository.GetUsers(filter);
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<long> Post(User model)
    {
      var passwordDecrypt = EncryptPasswords.DecryptWithAes(model.clave, model.persona.correo);
      var encryptPassword = EncryptPasswords.EncryptWithHash(passwordDecrypt);
      model.clave = encryptPassword;
      long result = _userRepository.Post(model);
      return new JsonResult(result);
    }
  }
}
