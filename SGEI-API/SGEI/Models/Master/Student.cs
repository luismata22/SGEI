using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SGEI.Models.Master
{
  public class Student
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    public string nombres { get; set; }

    public string apellidos { get; set; }

    public int idtipocurso { get; set; }

    public DateTime fechanacimiento { get; set; }

    public DateTime fecharegistro { get; set; }

    public List<Person> representantes { get; set; }
  }
}