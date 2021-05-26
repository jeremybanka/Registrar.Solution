using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Registrar.Models;

namespace Registrar.Controllers
{
  public class CourseStudentsController : Controller
  {
    private readonly RegistrarContext _db;

    public CourseStudentsController(RegistrarContext db)
    {
      _db = db;
    }
    public CourseStudent FindCourseStudent(int id) => _db.CourseStudents
      .FirstOrDefault(cs => cs.CourseStudentId == id);

    public CourseStudent CheckEnrollment(int courseId, int studentId) => _db.CourseStudents
      .FirstOrDefault(cs => (cs.StudentId == studentId && cs.StudentId == courseId));

    public void CreateNewStudentCourse(int courseId, int studentId) => _db.CourseStudents.Add(new CourseStudent() { CourseId = courseId, StudentId = studentId });


    [HttpPost]
    public ActionResult Delete(int id, string redirectTo)
    {
      var thisCourseStudent = FindCourseStudent(id);
      _db.CourseStudents.Remove(thisCourseStudent);
      _db.SaveChanges();
      int entityId = 0;
      if (redirectTo == "Courses") entityId = thisCourseStudent.CourseId;
      if (redirectTo == "Students") entityId = thisCourseStudent.StudentId;

      return RedirectToAction("Edit", redirectTo, new { id = entityId });
    }

    [HttpPost]
    public ActionResult Create(int id, int CourseId, string redirectTo)
    {
      bool studentIsTakingCourse = CheckEnrollment(id, CourseId) != null;
      if (CourseId != 0 && !studentIsTakingCourse)
      {
        CreateNewStudentCourse(CourseId, id);
      }
      return RedirectToAction("Edit", redirectTo, new { id = id });
    }
  }
}