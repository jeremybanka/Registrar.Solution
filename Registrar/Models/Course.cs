using System.Collections.Generic;

namespace Registrar.Models
{
  public class Course
  {
    public Course()
    {
      Enrollments = new HashSet<Enrollment>();
    }
    public string Name { get; set; }
    public int CourseId { get; set; }
    public int Number { get; set; }
    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; }
  }
}