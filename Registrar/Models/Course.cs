using System.Collections.Generic;

namespace Registrar.Models
{
  public class Course
  {
    public Course()
    {
      CourseStudents = new HashSet<CourseStudent>();
    }
    public string Name { get; set; }
    public int CourseId { get; set; }
    public int Number { get; set; }
    public virtual ICollection<CourseStudent> CourseStudents { get; set; }
  }
}