using Microsoft.AspNetCore.Mvc;

namespace MCC75_MVC.Controllers;

public class ErrorController : Controller
{
    [Route("Unauthorized")]
    public IActionResult Unauthorized()
    {
        return View();
    }

    [Route("Forbidden")]
    public IActionResult Forbidden()
    {
        return View();
    }
}
