using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Registrar.Models;

namespace Registrar.Controllers
{
  public class EnrollmentsController : Controller
  {
    private readonly RegistrarContext _db;

    public EnrollmentsController(RegistrarContext db)
    {
      _db = db;
    }
    public Enrollment FindEnrollment(int id) => _db.Enrollments
      .FirstOrDefault(e => e.EnrollmentId == id);

    public Enrollment CheckEnrollment(int courseId, int studentId)
    {
      Console.WriteLine($"check courseId {courseId}");
      Console.WriteLine($"check studentId {studentId}");
      var maybeCourse = _db.Enrollments
        .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
      Console.WriteLine($"maybeEnrollment {maybeCourse}");
      return maybeCourse;
    }

    public void CreateNewStudentCourse(int courseId, int studentId)
    {
      _db.Enrollments.Add(new Enrollment() { CourseId = courseId, StudentId = studentId });
      _db.SaveChanges();

    }


    [HttpPost]
    public ActionResult Delete(int id, string origin)
    {
      Console.WriteLine($"origin: {origin}");
      var e = FindEnrollment(id);
      _db.Enrollments.Remove(e);
      _db.SaveChanges();
      int entityId = 0;
      if (origin == "Courses") entityId = e.CourseId;
      if (origin == "Students") entityId = e.StudentId;

      return RedirectToAction("Edit", origin, new { id = entityId });
    }

    [HttpPost]
    public ActionResult Create(int StudentId, int CourseId, string origin)
    {
      Console.WriteLine($"origin: {origin}");
      Console.WriteLine($"CourseId: {CourseId}");
      Console.WriteLine($"StudentId: {StudentId}");
      bool studentIsNotTakingCourse = CheckEnrollment(CourseId, StudentId) == null;
      if (CourseId != 0 && studentIsNotTakingCourse)
      {
        CreateNewStudentCourse(CourseId, StudentId);
      }
      int entityId = 0;
      if (origin == "Courses") entityId = CourseId;
      if (origin == "Students") entityId = StudentId;

      return RedirectToAction("Edit", origin, new { id = entityId });
    }
  }
}