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

    public List<Permission> GetPermissions()
    {
      var permissions = _context.permisos.ToList();
      return permissions;
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
          

          if (model.permisosxrole.Count > 0)
          {
            if (model.id > 0)
            {
              _context.permisosxroles.RemoveRange(_context.permisosxroles.Where(x => x.idrol == model.id));
              _context.SaveChanges();
            }
            foreach (PermissionxRole permission in model.permisosxrole)
            {
              var permissionxrol = new PermissionxRole
              {
                idrol = model.id,
                idpermiso = permission.idpermiso,
                activo = true,
              };
              _context.permisosxroles.Add(permissionxrol);
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
      if (!String.IsNullOrEmpty(filter.Nombre) && filter.Activo > -1)
      {
        var roles = _context.roles.Where(x => x.nombre.ToLower().Contains(filter.Nombre.ToLower()) && (filter.Activo == 1 ? true : false)).ToList();
        foreach (var role in roles)
        {
          role.permisosxrole = _context.permisosxroles.Where(x => x.idrol == role.id).Include(x => x.permiso).ToList();
        }
        return roles;
      }
      else if (String.IsNullOrEmpty(filter.Nombre) && filter.Activo > -1)
      {
        var roles = _context.roles.Where(x => x.activo == (filter.Activo == 1 ? true : false)).ToList();
        foreach (var role in roles)
        {
          role.permisosxrole = _context.permisosxroles.Where(x => x.idrol == role.id).Include(x => x.permiso).ToList();
        }
        return roles;
      }
      else if (!String.IsNullOrEmpty(filter.Nombre) && filter.Activo == -1)
      {
        var roles = _context.roles.Where(x => x.nombre.ToLower().Contains(filter.Nombre.ToLower())).ToList();
        foreach (var role in roles)
        {
          role.permisosxrole = _context.permisosxroles.Where(x => x.idrol == role.id).Include(x => x.permiso).ToList();
        }
        return roles;
      }
      else
      {
        var roles = _context.roles.ToList();
        foreach (var role in roles)
        {
          role.permisosxrole = _context.permisosxroles.Where(x => x.idrol == role.id).Include(x => x.permiso).ToList();
        }
        return roles;
      }
    }
  }
}
