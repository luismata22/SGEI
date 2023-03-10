using SGEI.Models.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGEI.Models
{
  public class Role
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public string nombre { get; set; }

    public string descripcion { get; set; }

    public bool activo { get; set; }

    public string key { get; set; }

    public List<PermissionxModulexRole> permisosxmoduloxrole { get; set; }
  }
}
