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
using SGEI.Models.Security;

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
      var users = _context.usuarios.Include(x => x.persona).ToListAsync();

      if (users.Result.ToList().FindAll(x => x.persona.correo == userName && x.clave == password).Count > 0)
        return users.Result.ToList().Find(x => x.persona.correo == userName && x.clave == password);
      else
        return null;
    }

    public long ResetPassword(ResetPassword model)
    {
      try
      {
        var users = _context.usuarios.Include(x => x.persona).ToListAsync();

        if (users.Result.FindAll(x => x.persona.correo == model.User).Count > 0)
        {
          const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
          var random = new Random();
          var code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

          var mailRequest = new MailRequest();
          mailRequest.ToEmail = model.User;
          mailRequest.Subject = "Código de seguridad (Resstablecer contraseña)";
          mailRequest.Body = "<html>\r\n    <head>\r\n\r\n    </head>\r\n    <body>\r\n        <p>Hola</p><br>\r\n        <p>Este es el código de seguridad para cambiar la contraseña: " + code + "</p>\r\n    </body>\r\n</html>";
          _mailService.SendEmailAsync(mailRequest);


          var user = users.Result.ToList().Find(x => x.persona.correo == model.User);
          var data = new CodesResetPasswordxUsers()
          {
            idusuario = user.id,
            codigo = code,
            activo = true
          };
          if (_context.codigoresetearpasswordxusuario.ToList().FindAll(x => x.idusuario == user.id && x.activo == true).Count > 0)
          {
            var codeActive = _context.codigoresetearpasswordxusuario.ToList().Find(x => x.idusuario == user.id && x.activo == true);
            codeActive.activo = false;
            _context.codigoresetearpasswordxusuario.Add(codeActive);
            _context.Entry(codeActive).State = EntityState.Modified;
            _context.SaveChanges();
          }

          _context.codigoresetearpasswordxusuario.Add(data);
          _context.SaveChanges();
          return 1; //success
        }
        else
        {
          return -1; //no exist user
        }


      }
      catch (Exception ex)
      {
        return -2; //error general
      }

    }

    public long ValidateCode(ResetPassword model)
    {
      try
      {
        var users = _context.usuarios.Include(x => x.persona).ToListAsync();
        if (_context.codigoresetearpasswordxusuario.ToList().FindAll(x => x.idusuario == users.Result.ToList().Find(x => x.persona.correo == model.User).id && x.codigo == model.Code && x.activo == true).Count > 0)
        {
          return 1; //success
        }
        else
        {
          return -1; //code not valid
        }
      }
      catch (Exception ex)
      {
        return -2; //error general
      }

    }

    public bool UpdatePassword(ResetPassword model)
    {
      try
      {
        var users = _context.usuarios.Include(x => x.persona).ToListAsync();
        var user = users.Result.Find(x => x.persona.correo == model.User);
        user.clave = model.Password;
        _context.usuarios.Add(user);
        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();

        var code = _context.codigoresetearpasswordxusuario.ToList().Find(x => x.idusuario == user.id && x.activo == true);
        code.activo = false;
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

    public List<PermissionxModule> GetModules()
    {
      try
      {
        var modulos = _context.modulos.Where(x => x.idpadre == 0).ToList();
        var permisosxmodulo = _context.permisosxmodulo.Where(x => x.idpermiso == 2).Include(x => x.modulo).ToList();
        foreach (var modulo in modulos)
        {
          var item = new PermissionxModule
          {
            modulo = modulo,
          };
          permisosxmodulo.Add(item);
        }
        return permisosxmodulo;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public List<PermissionxModule> GetPermissionsxUser(long idUser)
    {
      try
      {
        List<PermissionxModule> permissionsxmodule = new List<PermissionxModule>();
        var rolesxusuario = _context.rolesxusuario.Where(rxu => rxu.idusuario == idUser).ToList();
        if (rolesxusuario.Count > 0)
        {
          rolesxusuario.ForEach(roles =>
          {
            var permisosxmoduloxrol = _context.permisosxmoduloxroles.Where(x => x.idrol == roles.idrol).ToList();
            if (permisosxmoduloxrol.Count > 0)
            {
              permisosxmoduloxrol.ForEach(permisosxmodulo =>
              {
                var permisoxmodulo = new PermissionxModule
                {
                  id = permisosxmodulo.idpermisoxmodulo,
                };
                permissionsxmodule.Add(permisoxmodulo);
              });
            }
          });
        }
        return permissionsxmodule.Distinct().ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
