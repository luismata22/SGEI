using Microsoft.EntityFrameworkCore;
using SGEI.Models.Master;
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

    public bool activo { get; set; }

    public string clave { get; set; }

    public long idpersona { get; set; }

    public List<RolesxUsuario> rolesxusuario { get; set; }

    [ForeignKey(nameof(idpersona))]
    public Person persona { get; set; }
  }
}
