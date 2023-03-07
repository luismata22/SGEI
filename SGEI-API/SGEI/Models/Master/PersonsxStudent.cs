using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SGEI.Models.Master
{
  public class PersonsxStudent
  {
    [Key]
    public long id { get; set; }

    public long idpersona { get; set; }

    public long idestudiante { get; set; }

    public bool activo { get; set; }

    [ForeignKey(nameof(idpersona))]
    public Person persona { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(idestudiante))]
    public Student estudiante { get; set; }

    public bool esrepresentante { get; set; }
  }
}
