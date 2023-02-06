using SGEI.Models;

namespace SGEI.Interfaces
{
  public interface ILoginRepository
  {
    User UserAuthenticate(string userName, string password);
  }
}
