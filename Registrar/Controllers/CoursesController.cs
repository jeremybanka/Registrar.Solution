using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Registrar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Registrar.Controllers
{
  public class CoursesController : Controller
  {
    private readonly RegistrarContext _db;

    public CoursesController(RegistrarContext db)
    {
      _db = db;
    }

    public List<Course> AllCourses() => _db.Courses.ToList();
    public Course FindCourse(int id) => _db.Courses
      .Include(course => course.Enrollments)
      .ThenInclude(join => join.Student)
      .FirstOrDefault(course => course.CourseId == id);

    public ActionResult Index() => View(AllCourses());

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Course c)
    {
      _db.Courses.Add(c);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id) => View(FindCourse(id));

    public ActionResult Edit(int id, string controller)
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      ViewBag.Controller = controller;
      return View(FindCourse(id));
    }
    [HttpPost]
    public ActionResult Edit(Course c)
    {
      _db.Entry(c).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id) => View(FindCourse(id));

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      _db.Courses.Remove(thisCourse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}