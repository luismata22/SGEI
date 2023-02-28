using SGEI.Models;
using SGEI.Models.Authenticate;
using SGEI.Models.Security;
using System.Collections.Generic;

namespace SGEI.Interfaces
{
  public interface ILoginRepository
  {
    User UserAuthenticate(string userName, string password);

    long ResetPassword(ResetPassword model);

    long ValidateCode(ResetPassword model);

    bool UpdatePassword(ResetPassword model);

    List<PermissionxModule> GetModules();

    List<PermissionxModule> GetPermissionsxUser(long idUser);
  }
}
