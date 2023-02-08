using SGEI.Models;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using SGEI.Context;
using SGEI.Models.Authenticate;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SGEI.Interfaces;
using System;
using SGEI.Models.Utils;
using SGEI.Repository.mail;
using SGEI.Interfaces.mail;

namespace SGEI.Repository
{
  public class LoginRepository : ILoginRepository
  {

    private readonly SGEIContext _context;
    private readonly IMailService _mailService;
    public LoginRepository(SGEIContext context, IMailService mailService)
    {
      _context = context;
      _mailService = mailService;
    }

    public User UserAuthenticate(string userName, string password)
    {
      var users = _context.usuarios.ToListAsync();

      if (users.Result.ToList().FindAll(x => x.Correo == userName && x.Clave == password).Count > 0)
        return users.Result.ToList().Find(x => x.Correo == userName && x.Clave == password);
      else
        return null;
    }

    public bool ResetPassword(ResetPassword model)
    {
      try
      {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var random = new Random();
        var code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

        var mailRequest = new MailRequest();
        mailRequest.ToEmail = model.User;
        mailRequest.Subject = "Código de seguridad (Resstablecer contraseña)";
        mailRequest.Body = "<html>\r\n    <head>\r\n\r\n    </head>\r\n    <body>\r\n        <p>Hola</p><br>\r\n        <p>Este es el código de seguridad para cambiar la contraseña: " + code + "</p>\r\n    </body>\r\n</html>";
        _mailService.SendEmailAsync(mailRequest);


        var users = _context.usuarios.ToListAsync();
        var data = new CodesResetPasswordxUsers()
        {
          IdUsuario = users.Result.ToList().Find(x => x.Correo == model.User).Id,
          Codigo = code,
          Activo = true
        };
        _context.codigoresetearpasswordxusuario.Add(data);
        _context.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
      
    }

    public bool ValidateCode(ResetPassword model)
    {
      try
      {
        var users = _context.usuarios.ToListAsync();
        if(_context.codigoresetearpasswordxusuario.ToList().FindAll(x => x.IdUsuario == users.Result.ToList().Find(x => x.Correo == model.User).Id && x.Codigo == model.Code && x.Activo == true).Count > 0)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (Exception ex)
      {
        return false;
      }

    }

    public bool UpdatePassword(ResetPassword model)
    {
      try
      {
        var users = _context.usuarios.ToListAsync();
        var user = users.Result.Find(x => x.Correo == model.User);
        user.Clave = model.Password;
        _context.usuarios.Add(user);
        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();

        var code = _context.codigoresetearpasswordxusuario.ToList().Find(x => x.IdUsuario == user.Id && x.Activo == true);
        code.Activo = false;
        _context.codigoresetearpasswordxusuario.Add(code);
        _context.Entry(code).State = EntityState.Modified;
        _context.SaveChanges();

        return true;
      }
      catch (Exception ex)
      {
        return false;
      }

    }
  }
}
