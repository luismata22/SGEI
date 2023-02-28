namespace SGEI.Models.Security
{
  public class Module
  {
    public int id { get; set; }

    public string nombre { get; set; }

    public int idpadre { get; set; }

    public string url { get; set; }

    public string icon { get; set; }

    public bool activo { get; set; }
  }
}
