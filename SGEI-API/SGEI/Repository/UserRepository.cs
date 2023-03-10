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
      var usuarios = _context.usuarios.Include(x => x.persona).Where(x => x.persona.nombres.ToLower().Contains(filter.Nombre == null ? "" : filter.Nombre.ToLower()) && x.persona.correo.ToLower().Contains(filter.Correo == null ? "" : filter.Correo.ToLower()) && x.persona.cedula.ToLower().Contains(filter.Cedula == null ? "" : filter.Cedula.ToLower()) && (filter.Activo == -1 || (x.activo.Equals(filter.Activo == 1 ? true : false)))).ToList();
      foreach (var usuario in usuarios)
      {
        usuario.rolesxusuario = _context.rolesxusuario.Where(x => x.idusuario == usuario.id).Include(x => x.rol).ToList();
      }
      return usuarios;
    }

    public User GetUserByIdPerson(long idPerson)
    {
      var user = _context.usuarios.Where(x => x.idpersona.Equals(idPerson)).FirstOrDefault();
      return user;
    }

    public long Post(User model)
    {
      try
      {
        if (model.id > 0 || (model.id <= 0 && _context.usuarios.Include(x => x.persona).Where(x => x.persona.correo == model.persona.correo).ToList().Count == 0))
        {
          if (model.id > 0)
          {
            _context.personas.Add(model.persona);
            _context.SaveChanges();

            var user = new User
            {
              id = model.id,
              clave = model.clave,
              idpersona = model.idpersona,
              activo = model.activo,
            };
            _context.ChangeTracker.Clear();
            _context.Update(user);
            _context.SaveChanges();
          }
          else
          {
            if (model.persona.id <= 0)
            {
              model.persona.id = 0;
              _context.personas.Add(model.persona);
              _context.SaveChanges();
            }

            var user = new User
            {
              idpersona = model.persona.id,
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
              if (_context.rolesxusuario.Where(x => x.idusuario == model.id).ToList().Count > 0)
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
