namespace Registrar.Models
{
  public class Major
  {
    public int MajorId { get; set; }
    public int Credits { get; set; }
    public int DepartmentId { get; set; }
    public int StudentId { get; set; }

    public virtual Department Department { get; set; }
    public virtual Student Student { get; set; }
  }
}