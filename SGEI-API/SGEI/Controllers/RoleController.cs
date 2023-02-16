using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGEI.Interfaces;
using SGEI.Models;
using SGEI.Models.Security;
using SGEI.Models.Security.Filters;
using System.Collections.Generic;

namespace SGEI.Controllers
{
  [ApiController]
  [Route("[controller]")]
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

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<long> Post(Role model)
    {
      long result = _roleRepository.Post(model);
      return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetRoles")]
    public ActionResult<List<Role>> GetRoles([FromQuery] RoleFilters filter)
    {
      List<Role> result = _roleRepository.GetRoles(filter);
      return new JsonResult(result);
    }
  }
}
