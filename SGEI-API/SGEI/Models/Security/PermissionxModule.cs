using System.ComponentModel.DataAnnotations.Schema;

namespace SGEI.Models.Security
{
  public class PermissionxModule
  {
    public long id { get; set; }

    public int idmodulo { get; set; }

    public int idpermiso { get; set; }

    public bool activo { get; set; }

    [ForeignKey(nameof(idmodulo))]
    public Module modulo { get; set; }

    [ForeignKey(nameof(idpermiso))]
    public Permission permiso { get; set; }
  }
}
