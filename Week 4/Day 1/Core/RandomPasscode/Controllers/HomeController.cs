using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;


namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {    if(HttpContext.Session.GetInt32("nbr")==null){
        HttpContext.Session.SetInt32("nbr",0);
    }

        return View();
    }
  
    [HttpPost("generate")]
    public IActionResult generate(int nbr)
    { int? initial= HttpContext.Session.GetInt32("nbr") ;
        HttpContext.Session.SetInt32("nbr",1+(int)initial);
      
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
