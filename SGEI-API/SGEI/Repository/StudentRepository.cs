using SGEI.Context;
using SGEI.Interfaces;
using SGEI.Interfaces.mail;
using SGEI.Models;
using SGEI.Models.Master;
using SGEI.Models.Security;
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

    public long Post(Student model)
    {
      try
      {
        if (model.id > 0)
        {
          _context.ChangeTracker.Clear();
          _context.Update(model);
          _context.SaveChanges();

          if (model.representantes.Count > 0)
          {
            foreach (Person representante in model.representantes)
            {
              if (representante.id > 0)
              {
                _context.ChangeTracker.Clear();
                _context.Update(representante);
                _context.SaveChanges();
              }
              else
              {
                _context.personas.Add(representante);
                _context.SaveChanges();
                var personxStudent = new PersonsxStudent
                {
                  idpersona = representante.id,
                  idestudiante = model.id,
                  activo = true,
                };
                _context.personasxestudiante.Add(personxStudent);
                _context.SaveChanges();
              }
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
            fecharegistro = model.fecharegistro,
          };
          _context.estudiantes.Add(student);
          _context.SaveChanges();
          model.id = student.id;

          if (model.representantes.Count > 0)
          {
            if (model.id > 0)
            {
              foreach (Person representante in model.representantes)
              {
                var person = new Person
                {
                  nombres = representante.nombres,
                  apellidos = representante.apellidos,
                  cedula = representante.cedula,
                  fechanacimiento = representante.fechanacimiento,
                  telefono = representante.telefono,
                  correo = representante.correo,
                  direccion = representante.direccion,
                  esrepresentante = representante.esrepresentante,
                  profesion = representante.profesion
                };
                _context.personas.Add(person);
                _context.SaveChanges();

                var personxStudent = new PersonsxStudent
                {
                  idpersona = person.id,
                  idestudiante = representante.id,
                  activo = true,
                };

                _context.personasxestudiante.Add(personxStudent);
                _context.SaveChanges();

                if (representante.id <= 0 && representante.esrepresentante)
                {
                  const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                  var random = new Random();
                  var code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                  var passwordDecrypt = EncryptPasswords.DecryptWithAes(code, representante.correo);
                  var encryptPassword = EncryptPasswords.EncryptWithHash(passwordDecrypt);

                  var mailRequest = new MailRequest();
                  mailRequest.ToEmail = representante.correo;
                  mailRequest.Subject = "Código de seguridad (Resstablecer contraseña)";
                  mailRequest.Body = "<html>\r\n    <head>\r\n\r\n    </head>\r\n    <body>\r\n        <p>Hola</p><br>\r\n        <p>Este es el código de seguridad para cambiar la contraseña: " + code + "</p>\r\n    </body>\r\n</html>";
                  _mailService.SendEmailAsync(mailRequest);
                  var user = new User
                  {
                    nombres = representante.nombres,
                    apellidos = representante.apellidos,
                    correo = representante.correo,
                    cedula = representante.cedula,
                    clave = encryptPassword,
                    activo = true,
                  };
                  _context.usuarios.Add(user);
                  _context.SaveChanges();

                  var role = _context.roles.Where(x => x.key == "repre").FirstOrDefault();
                  var rolesxusuario = new RolesxUsuario
                  {
                    idusuario = model.id,
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


        return model.id;
      }
      catch (System.Exception ex)
      {
        throw ex;
      }
    }
  }
}
