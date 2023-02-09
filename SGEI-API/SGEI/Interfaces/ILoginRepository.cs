using SGEI.Models;
using SGEI.Models.Authenticate;

namespace SGEI.Interfaces
{
  public interface ILoginRepository
  {
    User UserAuthenticate(string userName, string password);

    long ResetPassword(ResetPassword model);

    long ValidateCode(ResetPassword model);

    bool UpdatePassword(ResetPassword model);
  }
}
