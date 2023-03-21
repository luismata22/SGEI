using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SGEI.Models.Master
{
  public class FilesxStudents
  {
    public long id { get; set; }

    public long idestudiante { get; set; }

    [NotMapped]
    public IFormFile file { get; set; }

    public string nombre { get; set; }

    public decimal peso { get; set; }

    public bool indperfil { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(idestudiante))]
    public Student estudiante { get; set; }
  }
}
