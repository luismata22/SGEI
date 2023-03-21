using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SGEI.Models.Master
{
  public class Student
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public string nombres { get; set; }

    public string apellidos { get; set; }

    public int idtipocurso { get; set; }

    public DateTime fechanacimiento { get; set; }

    public DateTime fecharegistro { get; set; }

    public bool activo { get; set; }

    [ForeignKey(nameof(idtipocurso))]
    public TypeCourse tipocurso { get; set; }

    public List<PersonsxStudent> personasxestudiante { get; set; }

    public List<FilesxStudents> archivosxestudiante { get; set; }
  }
}
