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

    public User UserAuthenticate(string userName, string password)
    {
      LoginContext context = new LoginContext();
      var users = context.usuarios.ToList();

      if (users.FindAll(x => x.Correo == userName && x.Clave == password).Count > 0)
        return users.Find(x => x.Correo == userName && x.Clave == password);
      else
        return null;
    }
  }
}
