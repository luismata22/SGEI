using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGEI.Interfaces;
using SGEI.Models.Security;
using System.Collections.Generic;

namespace SGEI.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class RoleController : ControllerBase
  {
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
      _roleRepository = roleRepository;
    }

    [AllowAnonymous]
    [HttpGet("GetPermissions")]
    public ActionResult<List<Permission>> GetPermissions()
    {
      List<Permission> result = _roleRepository.GetPermissions();
      return new JsonResult(result);
    }
  }
}
