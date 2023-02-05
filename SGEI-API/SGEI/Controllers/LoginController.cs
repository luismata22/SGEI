using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGEI.Models;

namespace SGEI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult<string> Authenticate(Login model)
        {
            //var authenticateResponse = _loginRepository.LoginUser(credentials);
            return new JsonResult(model);
        }
    }
}
