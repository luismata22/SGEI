using Microsoft.EntityFrameworkCore;
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
  public class UserRepository : IUserRepository
  {
    private readonly SGEIContext _context;
    public UserRepository(SGEIContext context)
    {
      _context = context;
    }

    public List<User> GetUsers(UserFilters filter)
    {
      /*if (!String.IsNullOrEmpty(filter.Nombre) && !String.IsNullOrEmpty(filter.Correo) && !String.IsNullOrEmpty(filter.Cedula) && filter.Activo > -1)
      {
        var usuarios = _context.usuarios.ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      }
      else if (String.IsNullOrEmpty(filter.Nombre) && !String.IsNullOrEmpty(filter.Correo) && !String.IsNullOrEmpty(filter.Cedula) && filter.Activo > -1)
      {
        var usuarios = _context.usuarios.ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      }
      else if (String.IsNullOrEmpty(filter.Nombre) && String.IsNullOrEmpty(filter.Correo) && !String.IsNullOrEmpty(filter.Cedula) && filter.Activo > -1)
      {
        var usuarios = _context.usuarios.ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      }
      else if (String.IsNullOrEmpty(filter.Nombre) && String.IsNullOrEmpty(filter.Correo) && String.IsNullOrEmpty(filter.Cedula) && filter.Activo > -1)
      {
        var usuarios = _context.usuarios.ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      }
      else if (String.IsNullOrEmpty(filter.Nombre) && !String.IsNullOrEmpty(filter.Correo) && String.IsNullOrEmpty(filter.Cedula) && filter.Activo > -1)
      {
        var usuarios = _context.usuarios.ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      }
      else if (String.IsNullOrEmpty(filter.Nombre) && !String.IsNullOrEmpty(filter.Correo) && !String.IsNullOrEmpty(filter.Cedula) && filter.Activo > -1)
      {
        var usuarios = _context.usuarios.ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      }
      else if (String.IsNullOrEmpty(filter.Nombre) && !String.IsNullOrEmpty(filter.Correo) && String.IsNullOrEmpty(filter.Cedula) && filter.Activo > -1)
      {
        var usuarios = _context.usuarios.ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      }
      else if (!String.IsNullOrEmpty(filter.Nombre) && filter.Activo == -1)
      {
        var usuarios = _context.usuarios.ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      }
      else
      {*/
        var usuarios = _context.usuarios.Include(x => x.persona).ToList();
        foreach (var usuario in usuarios)
        {
          usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
        }
        return usuarios;
      //}
      
    }

    public long Post(User model)
    {
      try
      {
        if (model.id > 0 || (model.id <= 0 && _context.usuarios.Include(x => x.persona).Where(x => x.persona.correo == model.persona.correo).ToList().Count == 0))
        {
          if (model.id > 0)
          {
            var user = new User
            {
              id = model.id,
              //nombres = model.nombres,
              //apellidos = model.apellidos,
              //correo = model.correo,
              //cedula = model.cedula,
              clave = model.clave,
              activo = model.activo,
            };
            _context.ChangeTracker.Clear();
            _context.Update(user);
            _context.SaveChanges();
          }
          else
          {
            var user = new User
            {
              //nombres = model.nombres,
              //apellidos = model.apellidos,
              //correo = model.correo,
              //cedula = model.cedula,
              clave = model.clave,
              activo = true,
            };
            _context.usuarios.Add(user);
            _context.SaveChanges();
            model.id = user.id;
          }


          if (model.rolesxusuario.Count > 0)
          {
            if (model.id > 0)
            {
              if(_context.rolesxusuario.Where(x => x.idusuario == model.id).ToList().Count > 0)
              {
                _context.rolesxusuario.RemoveRange(_context.rolesxusuario.Where(x => x.idusuario == model.id));
                _context.SaveChanges();
              }
            }
            foreach (RolesxUsuario role in model.rolesxusuario)
            {
              var rolesxusuario = new RolesxUsuario
              {
                idusuario = model.id,
                idrol = role.idrol,
                activo = true,
              };
              _context.rolesxusuario.Add(rolesxusuario);
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
  }
}
