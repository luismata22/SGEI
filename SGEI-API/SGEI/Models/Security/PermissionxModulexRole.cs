using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SGEI.Models.Security
{
  public class PermissionxModulexRole
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public long idpermisoxmodulo { get; set; }

    public long idrol { get; set; }

    public bool activo { get; set; }
    
    [ForeignKey(nameof(idrol))]
    [JsonIgnore]
    public Role rol { get; set; }

    [ForeignKey(nameof(idpermisoxmodulo))]
    public PermissionxModule permisosxmodulo { get; set; }
  }
}
