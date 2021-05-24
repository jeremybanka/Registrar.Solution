using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Registrar.Models;

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
      .Include(course => course.CourseStudents)
      .ThenInclude(join => join.Student)
      .FirstOrDefault(course => course.CourseId == id);

    public ActionResult Index() => View(AllCourses());

    public ActionResult Create() => View();
    [HttpPost]
    public ActionResult Create(Course c)
    {
      _db.Courses.Add(c);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id) => View(FindCourse(id));

    public ActionResult Edit(int id) => View(FindCourse(id));
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