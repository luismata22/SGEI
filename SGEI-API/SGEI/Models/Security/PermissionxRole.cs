using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SGEI.Models.Security
{
  public class PermissionxRole
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public int idpermiso { get; set; }

    public long idrol { get; set; }

    public bool activo { get; set; }

    
    [ForeignKey(nameof(idrol))]
    [JsonIgnore]
    public Role rol { get; set; }

    [ForeignKey(nameof(idpermiso))]
    public Permission permiso { get; set; }
  }
}
