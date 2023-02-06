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

namespace SGEI.Repository
{
  public class LoginRepository : ILoginRepository
  {

    private readonly SGEIContext _context;
    public LoginRepository(SGEIContext context)
    {
      _context = context;
    }

    public User UserAuthenticate(string userName, string password)
    {
      var users = _context.usuarios.ToListAsync();

      if (users.Result.ToList().FindAll(x => x.Correo == userName && x.Clave == password).Count > 0)
        return users.Result.ToList().Find(x => x.Correo == userName && x.Clave == password);
      else
        return null;
    }
  }
}
