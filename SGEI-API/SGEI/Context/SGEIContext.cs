using Microsoft.EntityFrameworkCore;
using SGEI.Models;
using SGEI.Models.Authenticate;
using SGEI.Models.Security;

namespace SGEI.Context
{
  public class SGEIContext : DbContext
  {
    public SGEIContext(DbContextOptions<SGEIContext> options) : base(options)
    {

    }
    //Usuarios
    public DbSet<User> usuarios => Set<User>();
    //Codigos de resetar contrasenas por usuario
    public DbSet<CodesResetPasswordxUsers> codigoresetearpasswordxusuario => Set<CodesResetPasswordxUsers>();
    //Permisos
    public DbSet<Permission> permisos => Set<Permission>();
    //Permisos x modulo
    public DbSet<Permission> permisosxmodulo => Set<Permission>();
    //Roles
    public DbSet<Role> roles => Set<Role>();
    //PermisosxRoles
    public DbSet<PermissionxModulexRole> permisosxmoduloxroles => Set<PermissionxModulexRole>();
    //RolesxUsuario
    public DbSet<RolesxUsuario> rolesxusuario => Set<RolesxUsuario>();
  }
}
