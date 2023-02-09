using SGEI.Models.Security;
using System.Collections.Generic;

namespace SGEI.Interfaces
{
  public interface IRoleRepository
  {
    List<Permission> GetPermissions();
  }
}
