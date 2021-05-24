using Microsoft.AspNetCore.Mvc;

namespace Registrar.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index() => View();
  }
}