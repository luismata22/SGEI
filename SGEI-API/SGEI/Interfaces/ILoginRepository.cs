using SGEI.Models;
using SGEI.Models.Authenticate;

namespace SGEI.Interfaces
{
  public interface ILoginRepository
  {
    User UserAuthenticate(string userName, string password);

    bool ResetPassword(ResetPassword model);

    bool ValidateCode(ResetPassword model);

    bool UpdatePassword(ResetPassword model);
  }
}
