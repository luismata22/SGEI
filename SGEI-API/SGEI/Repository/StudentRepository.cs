using Microsoft.EntityFrameworkCore;
using SGEI.Context;
using SGEI.Interfaces;
using SGEI.Interfaces.mail;
using SGEI.Models;
using SGEI.Models.Master;
using SGEI.Models.Master.Filters;
using SGEI.Models.Security;
using SGEI.Models.Security.Filters;
using SGEI.Models.Utils;
using SGEI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGEI.Repository
{
  public class StudentRepository : IStudentRepository
  {
    private readonly SGEIContext _context;
    private readonly IMailService _mailService;
    public StudentRepository(SGEIContext context, IMailService mailService)
    {
      _context = context;
      _mailService = mailService;
    }

    public List<TypeCourse> GetTypeCourse()
    {
      var typeCourses = _context.tipocurso.ToList();
      return typeCourses;
    }

    public List<Student> GetStudents(StudentFilters filter)
    {
      //if (!String.IsNullOrEmpty(filter.Nombre) && filter.Activo > -1)
      //{
      //  var roles = _context.roles.Where(x => x.nombre.ToLower().Contains(filter.Nombre.ToLower()) && (filter.Activo == 1 ? true : false)).ToList();
      //  foreach (var role in roles)
      //  {
      //    role.permisosxmoduloxrole = _context.permisosxmoduloxroles.Where(x => x.idrol == role.id)
      //      .Include(x => x.permisosxmodulo).ThenInclude(x => x.permiso)
      //      .Include(x => x.permisosxmodulo).ThenInclude(x => x.modulo).ToList();
      //  }
      //  return roles;
      //}
      //else if (String.IsNullOrEmpty(filter.Nombre) && filter.Activo > -1)
      //{
      //  var roles = _context.roles.Where(x => x.activo == (filter.Activo == 1 ? true : false)).ToList();
      //  foreach (var role in roles)
      //  {
      //    role.permisosxmoduloxrole = _context.permisosxmoduloxroles.Where(x => x.idrol == role.id)
      //      .Include(x => x.permisosxmodulo).ThenInclude(x => x.permiso)
      //      .Include(x => x.permisosxmodulo).ThenInclude(x => x.modulo).ToList();
      //  }
      //  return roles;
      //}
      //else if (!String.IsNullOrEmpty(filter.Nombre) && filter.Activo == -1)
      //{
      //  var roles = _context.roles.Where(x => x.nombre.ToLower().Contains(filter.Nombre.ToLower())).ToList();
      //  foreach (var role in roles)
      //  {
      //    role.permisosxmoduloxrole = _context.permisosxmoduloxroles.Where(x => x.idrol == role.id)
      //      .Include(x => x.permisosxmodulo).ThenInclude(x => x.permiso)
      //      .Include(x => x.permisosxmodulo).ThenInclude(x => x.modulo).ToList();
      //  }
      //  return roles;
      //}
      //else
      //{
      var estudiantes = _context.estudiantes.Where(x => x.nombres.ToLower().Contains(filter.nombre == null ? "" : filter.nombre.ToLower()) && x.apellidos.ToLower().Contains(filter.apellido == null ? "" : filter.apellido.ToLower()) && (filter.idtipocurso == -1 || x.idtipocurso == filter.idtipocurso)).Include(x => x.tipocurso).ToList();
      //foreach (var estudiante in estudiantes)
      //{
      //estudiante.permisosxmoduloxrole = _context.permisosxmoduloxroles.Where(x => x.idrol == role.id)
      //    .Include(x => x.permisosxmodulo).ThenInclude(x => x.permiso)
      //    .Include(x => x.permisosxmodulo).ThenInclude(x => x.modulo).ToList();

      //  //role.permisosxmoduloxrole = role.permisosxmoduloxrole.GroupBy(x => x.permisosxmodulo.modulo).Select(d => d.First()).ToList();
      //}
      return estudiantes;
      //}
    }

    public Student GetStudentById(long idStudent)
    {
      var student = _context.estudiantes.Where(x => x.id.Equals(idStudent)).Include(x => x.tipocurso).FirstOrDefault();
      student.personasxestudiante = _context.personasxestudiante.Where(x => x.idestudiante.Equals(student.id)).Include(p => p.persona).ToList();
      student.archivosxestudiante = _context.archivosxestudiante.Where(x => x.idestudiante.Equals(student.id)).ToList();
      return student;
    }

    public long Post(Student model)
    {
      try
      {
        if (model.id > 0)
        {
          var student = new Student
          {
            id = model.id,
            nombres = model.nombres,
            apellidos = model.apellidos,
            idtipocurso = model.idtipocurso,
            fechanacimiento = model.fechanacimiento,
            fecharegistro = model.fecharegistro,
            activo = model.activo,
          };
          _context.ChangeTracker.Clear();
          _context.estudiantes.Update(student);
          _context.SaveChanges();

          if (model.personasxestudiante.Count > 0)
          {
            _context.personasxestudiante.RemoveRange(_context.personasxestudiante.Where(x => x.idestudiante == model.id));
            _context.SaveChanges();
            foreach (PersonsxStudent personsxStudent in model.personasxestudiante)
            {
              var person = new Person();
              if (personsxStudent.persona.id <= 0)
              {
                person = new Person
                {
                  nombres = personsxStudent.persona.nombres,
                  apellidos = personsxStudent.persona.apellidos,
                  cedula = personsxStudent.persona.cedula,
                  fechanacimiento = personsxStudent.persona.fechanacimiento,
                  telefono = personsxStudent.persona.telefono,
                  correo = personsxStudent.persona.correo,
                  direccion = personsxStudent.persona.direccion,
                  profesion = personsxStudent.persona.profesion
                };
                _context.ChangeTracker.Clear();
                _context.personas.Add(person);
                _context.SaveChanges();
              }
              else
              {
                person = new Person
                {
                  nombres = personsxStudent.persona.nombres,
                  apellidos = personsxStudent.persona.apellidos,
                  cedula = personsxStudent.persona.cedula,
                  fechanacimiento = personsxStudent.persona.fechanacimiento,
                  telefono = personsxStudent.persona.telefono,
                  correo = personsxStudent.persona.correo,
                  direccion = personsxStudent.persona.direccion,
                  profesion = personsxStudent.persona.profesion
                };
                _context.ChangeTracker.Clear();
                _context.personas.Update(person);
                _context.SaveChanges();
              }
              var personxStudent = new PersonsxStudent
              {
                idpersona = personsxStudent.persona.id <= 0 ? person.id : personsxStudent.persona.id,
                idestudiante = model.id,
                esrepresentante = personsxStudent.esrepresentante,
                activo = true,
              };
              _context.ChangeTracker.Clear();
              _context.personasxestudiante.Add(personxStudent);
              _context.SaveChanges();

              if (personsxStudent.esrepresentante)
              {
                if (_context.usuarios.Where(x => x.idpersona.Equals(personsxStudent.persona.id <= 0 ? person.id : personsxStudent.persona.id)).ToList().Count == 0)
                {
                  const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                  var random = new Random();
                  var code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                  //var passwordDecrypt = EncryptPasswords.DecryptWithAes(code, representante.correo);
                  var encryptPassword = EncryptPasswords.EncryptWithHash(code);

                  var mailRequest = new MailRequest();
                  mailRequest.ToEmail = personsxStudent.persona.correo;
                  mailRequest.Subject = "Código de seguridad (Resstablecer contraseña)";
                  mailRequest.Body = "<html>\r\n    <head>\r\n\r\n    </head>\r\n    <body>\r\n        <p>Hola</p><br>\r\n        <p>Este es el código de seguridad para cambiar la contraseña: " + code + "</p>\r\n    </body>\r\n</html>";
                  _mailService.SendEmailAsync(mailRequest);
                  var user = new User
                  {
                    idpersona = personsxStudent.persona.id <= 0 ? person.id : personsxStudent.persona.id,
                    clave = encryptPassword,
                    activo = true,
                  };
                  _context.ChangeTracker.Clear();
                  _context.usuarios.Add(user);
                  _context.SaveChanges();

                  var role = _context.roles.Where(x => x.key == "repre").FirstOrDefault();
                  var rolesxusuario = new RolesxUsuario
                  {
                    idusuario = user.id,
                    idrol = role.id,
                    activo = true,
                  };
                  _context.rolesxusuario.Add(rolesxusuario);
                  _context.SaveChanges();
                }
              }
            }
          }
          if (model.archivosxestudiante.Count > 0)
          {
            foreach (var file in model.archivosxestudiante.Where(x => x.id > 0))
            {
              var filesxstudents = new FilesxStudents
              {
                id = file.id,
                idestudiante = file.idestudiante,
                nombre = file.nombre,
                peso = file.peso,
                indperfil = file.indperfil,
              };
              _context.ChangeTracker.Clear();
              _context.archivosxestudiante.Update(filesxstudents);
              _context.SaveChanges();
            }
          }
        }
        else
        {
          var student = new Student
          {
            nombres = model.nombres,
            apellidos = model.apellidos,
            idtipocurso = model.idtipocurso,
            fechanacimiento = model.fechanacimiento,
            fecharegistro = DateTime.Today,
            activo = true,
          };
          _context.ChangeTracker.Clear();
          _context.estudiantes.Add(student);
          _context.SaveChanges();
          model.id = student.id;

          if (model.personasxestudiante.Count > 0)
          {
            if (model.id > 0)
            {
              foreach (PersonsxStudent personsxStudent in model.personasxestudiante)
              {
                var person = new Person();
                if (personsxStudent.persona.id <= 0)
                {
                  person = new Person
                  {
                    nombres = personsxStudent.persona.nombres,
                    apellidos = personsxStudent.persona.apellidos,
                    cedula = personsxStudent.persona.cedula,
                    fechanacimiento = personsxStudent.persona.fechanacimiento,
                    telefono = personsxStudent.persona.telefono,
                    correo = personsxStudent.persona.correo,
                    direccion = personsxStudent.persona.direccion,
                    profesion = personsxStudent.persona.profesion
                  };
                  _context.ChangeTracker.Clear();
                  _context.personas.Add(person);
                  _context.SaveChanges();
                }

                var personxStudent = new PersonsxStudent
                {
                  idpersona = personsxStudent.persona.id <= 0 ? person.id : personsxStudent.persona.id,
                  idestudiante = student.id,
                  esrepresentante = personsxStudent.esrepresentante,
                  activo = true,
                };
                _context.ChangeTracker.Clear();
                _context.personasxestudiante.Add(personxStudent);
                _context.SaveChanges();

                if (personsxStudent.esrepresentante)
                {
                  if (_context.usuarios.Where(x => x.idpersona.Equals(personsxStudent.persona.id <= 0 ? person.id : personsxStudent.persona.id)).ToList().Count == 0)
                  {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    var random = new Random();
                    var code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                    //var passwordDecrypt = EncryptPasswords.DecryptWithAes(code, representante.correo);
                    var encryptPassword = EncryptPasswords.EncryptWithHash(code);

                    var mailRequest = new MailRequest();
                    mailRequest.ToEmail = personsxStudent.persona.correo;
                    mailRequest.Subject = "Código de seguridad (Resstablecer contraseña)";
                    mailRequest.Body = "<html>\r\n    <head>\r\n\r\n    </head>\r\n    <body>\r\n        <p>Hola</p><br>\r\n        <p>Este es el código de seguridad para cambiar la contraseña: " + code + "</p>\r\n    </body>\r\n</html>";
                    _mailService.SendEmailAsync(mailRequest);
                    var user = new User
                    {
                      idpersona = personsxStudent.persona.id <= 0 ? person.id : personsxStudent.persona.id,
                      clave = encryptPassword,
                      activo = true,
                    };
                    _context.ChangeTracker.Clear();
                    _context.usuarios.Add(user);
                    _context.SaveChanges();

                    var role = _context.roles.Where(x => x.key == "repre").FirstOrDefault();
                    var rolesxusuario = new RolesxUsuario
                    {
                      idusuario = user.id,
                      idrol = role.id,
                      activo = true,
                    };
                    _context.rolesxusuario.Add(rolesxusuario);
                    _context.SaveChanges();
                  }
                }
              }

            }
          }
        }


        return model.id;
      }
      catch (System.Exception ex)
      {
        throw ex;
      }
    }

    public long UploadFiles(FilesxStudents filesxStudent)
    {
      try
      {
        if (filesxStudent.id > 0)
        {
          var model = new FilesxStudents
          {
            id = filesxStudent.id,
            idestudiante = filesxStudent.idestudiante,
            nombre = filesxStudent.file.FileName,
            peso = filesxStudent.file.Length,
            indperfil = filesxStudent.indperfil,
          };
          _context.ChangeTracker.Clear();
          _context.archivosxestudiante.Update(model);
          _context.SaveChanges();
        }
        else
        {
          var model = new FilesxStudents
          {
            idestudiante = filesxStudent.idestudiante,
            nombre = filesxStudent.file.FileName,
            peso = filesxStudent.file.Length,
            indperfil = filesxStudent.indperfil,
          };
          _context.ChangeTracker.Clear();
          _context.archivosxestudiante.Add(model);
          _context.SaveChanges();
          filesxStudent.id = model.id;
        }
        return filesxStudent.id;
      }
      catch (Exception ex)
      {
        return -1;
      }
    }

    public List<FilesxStudents> GetFileByIdStudent(long idStudent)
    {
      var files = _context.archivosxestudiante.Where(x => x.idestudiante.Equals(idStudent)).ToList();
      return files;
    }

    public long DeleteFile(FilesxStudents filesxStudent)
    {
      try
      {
        _context.ChangeTracker.Clear();
        _context.archivosxestudiante.Remove(filesxStudent);
        _context.SaveChanges();
        return filesxStudent.id;
      }
      catch (Exception)
      {
        return -1;
      }
    }
  }
}
