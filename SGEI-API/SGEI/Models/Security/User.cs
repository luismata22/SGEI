using Microsoft.EntityFrameworkCore;
using SGEI.Models.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGEI.Models
{
  public class User
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public string nombres { get; set; }

    public string apellidos { get; set; }

    public string correo { get; set; }

    public string cedula { get; set; }

    public bool activo { get; set; }

    public string clave { get; set; }

    public List<RolesxUsuario> rolesxusuario { get; set; }
  }
}
