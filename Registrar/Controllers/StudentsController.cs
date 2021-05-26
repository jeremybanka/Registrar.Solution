using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Registrar.Models;

namespace Registrar.Controllers
{
  public class StudentsController : Controller
  {
    private readonly RegistrarContext _db;

    public StudentsController(RegistrarContext db)
    {
      _db = db;
    }

    public List<Student> AllStudents() => _db.Students.ToList();
    public Student FindStudent(int id) => _db.Students
      .Include(student => student.CourseStudents)
      .ThenInclude(join => join.Course)
      .FirstOrDefault(student => student.StudentId == id);

    public void CreateNewStudentCourse(int courseId, int studentId) => _db.CourseStudents.Add(new CourseStudent() { CourseId = courseId, StudentId = studentId });

    public ActionResult Index() => View(AllStudents());
    public ActionResult Create()
    {
      ViewBag.Courses = new SelectList(_db.Courses, "CourseId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student s, int CourseId)
    {
      _db.Students.Add(s);
      _db.SaveChanges();
      if (CourseId != 0)
      {
        CreateNewStudentCourse(CourseId, s.StudentId);
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id) => View(FindStudent(id));
    public ActionResult Edit(int id, string controller)
    {
      var thisStudent = FindStudent(id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      ViewBag.Controller = controller;
      return View(thisStudent);
    }
    [HttpPost]
    public ActionResult Edit(Student s)
    {
      _db.Entry(s).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id) => View(FindStudent(id));
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      _db.Students.Remove(thisStudent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}