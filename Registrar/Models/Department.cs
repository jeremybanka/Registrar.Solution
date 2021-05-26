using System.Collections.Generic;

namespace Registrar.Models
{
  public class Department
  {

    public Department()
    {
      Courses = new HashSet<Course>();
      Majors = new HashSet<Major>();
    }
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public virtual ICollection<Major> Majors { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
  }
}