using System;
using System.Collections.Generic;

namespace Registrar.Models
{

  public class Student
  {
    public Student()
    {
      CourseStudents = new HashSet<CourseStudent>();
    }

    public string Name { get; set; }
    public int StudentId { get; set; }
    public DateTime DateEnrolled { get; set; }
    public virtual ICollection<CourseStudent> CourseStudents { get; set; }
  }


}