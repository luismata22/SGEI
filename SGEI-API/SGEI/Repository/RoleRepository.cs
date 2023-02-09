using SGEI.Context;
using SGEI.Interfaces;
using SGEI.Models.Security;
using System.Collections.Generic;
using System.Linq;

namespace SGEI.Repository
{
  public class RoleRepository : IRoleRepository
  {
    private readonly SGEIContext _context;
    public RoleRepository(SGEIContext context)
    {
      _context = context;
    }

    public List<Permission> GetPermissions()
    {
      var permissions = _context.permisos.ToList();
      return permissions;
    }
  }
}
