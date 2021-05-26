using Microsoft.AspNetCore.Mvc;
using System;
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

    public CourseStudent CheckEnrollment(int courseId, int studentId)
    {
      Console.WriteLine($"check courseId {courseId}");
      Console.WriteLine($"check studentId {studentId}");
      var maybeCourse = _db.CourseStudents
        .FirstOrDefault(cs => cs.StudentId == studentId && cs.CourseId == courseId);
      Console.WriteLine($"maybeCourseStudent {maybeCourse}");
      return maybeCourse;
    }

    public void CreateNewStudentCourse(int courseId, int studentId)
    {
      _db.CourseStudents.Add(new CourseStudent() { CourseId = courseId, StudentId = studentId });
      _db.SaveChanges();

    }


    [HttpPost]
    public ActionResult Delete(int id, string origin)
    {
      Console.WriteLine($"origin: {origin}");
      var cs = FindCourseStudent(id);
      _db.CourseStudents.Remove(cs);
      _db.SaveChanges();
      int entityId = 0;
      if (origin == "Courses") entityId = cs.CourseId;
      if (origin == "Students") entityId = cs.StudentId;

      return RedirectToAction("Edit", origin, new { id = entityId });
    }

    [HttpPost]
    public ActionResult Create(int StudentId, int CourseId, string origin)
    {
      Console.WriteLine($"origin: {origin}");
      bool studentIsNotTakingCourse = CheckEnrollment(CourseId, StudentId) == null;
      if (CourseId != 0 && studentIsNotTakingCourse)
      {
        CreateNewStudentCourse(CourseId, StudentId);
      }
      return RedirectToAction("Edit", origin, new { id = StudentId });
    }
  }
}