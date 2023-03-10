using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SGEI.Context;
using SGEI.Interfaces;
using SGEI.Models;
using SGEI.Models.Security;
using SGEI.Models.Security.Filters;
using System;
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

    public List<PermissionxModule> GetPermissionsxModule()
    {
      var permissionsxmodule = _context.permisosxmodulo.Include(x => x.permiso).Include(x => x.modulo).ToList();
      return permissionsxmodule;
    }

    public long Post(Role model)
    {
      try
      {
        if (model.id > 0 || (model.id <= 0 && _context.roles.ToList().Where(x => x.nombre == model.nombre).ToList().Count == 0))
        {
          if (model.id > 0)
          {
            var role = new Role
            {
              id = model.id,
              nombre = model.nombre,
              descripcion = model.descripcion,
              key = model.key,
              activo = model.activo,
            };
            _context.ChangeTracker.Clear();
            _context.Update(role);
            _context.SaveChanges();
          }
          else
          {
            var role = new Role
            {
              nombre = model.nombre,
              descripcion = model.descripcion,
              activo = true,
            };
            _context.roles.Add(role);
            _context.SaveChanges();
            model.id = role.id;
          }


          if (model.permisosxmoduloxrole.Count > 0)
          {
            if (model.id > 0)
            {
              _context.permisosxmoduloxroles.RemoveRange(_context.permisosxmoduloxroles.Where(x => x.idrol == model.id));
              _context.SaveChanges();
            }
            foreach (PermissionxModulexRole permisosxmodulo in model.permisosxmoduloxrole)
            {
              var permissionxmodulexrol = new PermissionxModulexRole
              {
                idrol = model.id,
                idpermisoxmodulo = permisosxmodulo.idpermisoxmodulo,
                activo = true,
              };
              _context.permisosxmoduloxroles.Add(permissionxmodulexrol);
              _context.SaveChanges();
            }
          }
          return model.id;
        }
        else
        {
          return -1; //Rol repetido
        }
      }
      catch (System.Exception ex)
      {
        return -2; //Error general
      }

    }

    public List<Role> GetRoles(RoleFilters filter)
    {
      var roles = _context.roles.Where(x => x.nombre.ToLower().Contains(filter.Nombre == null ? "" : filter.Nombre.ToLower()) && (filter.Activo == -1 || x.activo == (filter.Activo == 1 ? true : false))).ToList();
      foreach (var role in roles)
      {
        role.permisosxmoduloxrole = _context.permisosxmoduloxroles.Where(x => x.idrol == role.id)
          .Include(x => x.permisosxmodulo).ThenInclude(x => x.permiso)
          .Include(x => x.permisosxmodulo).ThenInclude(x => x.modulo).ToList();
      }
      return roles;
    }
  }
}
