using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SGEI.Models.Security
{
  public class RolesxUsuario
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public long idusuario { get; set; }

    public long idrol { get; set; }

    public bool activo { get; set; }

    [ForeignKey(nameof(idusuario))]
    [JsonIgnore]
    public User usuario { get; set; }

    [ForeignKey(nameof(idrol))]
    public Role rol { get; set; }
  }
}
