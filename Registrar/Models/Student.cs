using System;
using System.Collections.Generic;

namespace Registrar.Models
{

  public class Student
  {
    public Student()
    {
      Enrollments = new HashSet<Enrollment>();
      Majors = new HashSet<Major>();
    }

    public string Name { get; set; }
    public int StudentId { get; set; }
    public DateTime DateEnrolled { get; set; }
    public virtual ICollection<Major> Majors { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; }
  }


}