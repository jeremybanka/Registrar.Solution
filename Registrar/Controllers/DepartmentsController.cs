using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Registrar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Registrar.Controllers
{
  public class DepartmentsController : Controller
  {
    private readonly RegistrarContext _db;

    public DepartmentsController(RegistrarContext db)
    {
      _db = db;
    }

    public List<Department> AllDepartments() => _db.Departments.ToList();
    public Department FindDepartment(int id) => _db.Departments
      .FirstOrDefault(d => d.DepartmentId == id);

    public ActionResult Index() => View(AllDepartments());

    public ActionResult Create() => View();
    [HttpPost]
    public ActionResult Create(Department d)
    {
      _db.Departments.Add(d);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id) => View(FindDepartment(id));

    public ActionResult Edit(int id, string controller)
    {
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      ViewBag.Controller = controller;
      return View(FindDepartment(id));
    }
    [HttpPost]
    public ActionResult Edit(Department d)
    {
      _db.Entry(d).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id) => View(FindDepartment(id));

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      _db.Departments.Remove(FindDepartment(id));
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}