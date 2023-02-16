using SGEI.Models;
using SGEI.Models.Security;
using SGEI.Models.Security.Filters;
using System.Collections.Generic;

namespace SGEI.Interfaces
{
  public interface IRoleRepository
  {
    List<Permission> GetPermissions();

    long Post(Role model);

    List<Role> GetRoles(RoleFilters filter);
  }
}
