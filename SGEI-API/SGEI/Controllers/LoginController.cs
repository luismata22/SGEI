using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGEI.Models.Authenticate;
using System.IO;
using System;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using SGEI.Interfaces;
using SGEI.Models;
using SGEI.Utils;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace SGEI.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    public IConfiguration AppSettings { get; }
    private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository)
    {
      _loginRepository = loginRepository;
    }


    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public ActionResult<string> Authenticate(Login model)
    {
      if (string.IsNullOrEmpty(model.User) || string.IsNullOrEmpty(model.Password))
        throw new InvalidCredentialException();
      var passwordDecrypt = EncryptPasswords.DecryptWithAes(model.Password, model.User);
      var encryptPassword = EncryptPasswords.EncryptWithHash(passwordDecrypt);
      var user = _loginRepository.UserAuthenticate(model.User, encryptPassword);

      if (user is null)
        throw new InvalidCredentialException();

      return new JsonResult(user);
    }
    
  }
}
