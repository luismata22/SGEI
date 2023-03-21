using Microsoft.EntityFrameworkCore;
using SGEI.Models;
using SGEI.Models.Authenticate;
using SGEI.Models.Master;
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
    //Modulos
    public DbSet<Module> modulos => Set<Module>();
    //Permisos x modulo
    public DbSet<PermissionxModule> permisosxmodulo => Set<PermissionxModule>();
    //Roles
    public DbSet<Role> roles => Set<Role>();
    //PermisosxRoles
    public DbSet<PermissionxModulexRole> permisosxmoduloxroles => Set<PermissionxModulexRole>();
    //RolesxUsuario
    public DbSet<RolesxUsuario> rolesxusuario => Set<RolesxUsuario>();
    //Tipo de curso
    public DbSet<TypeCourse> tipocurso => Set<TypeCourse>();
    //Estudiantes
    public DbSet<Student> estudiantes => Set<Student>();
    //Personas
    public DbSet<Person> personas => Set<Person>();
    //Personas x estudiante
    public DbSet<PersonsxStudent> personasxestudiante => Set<PersonsxStudent>();
    //Archivos por estudiante
    public DbSet<FilesxStudents> archivosxestudiante => Set<FilesxStudents>();
  }
}
