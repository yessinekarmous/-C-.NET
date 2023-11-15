using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DSValidation.Models;
using FirstMVC.Models;
namespace DSValidation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

     [HttpPost]       //type of request
        [Route("result")]     //associated route string (exclude the leading /)
        public IActionResult Index(User newUser)
        {
            
            return View("Result",newUser);
        }

        [HttpGet]       //type of request
        [Route("result")]     //associated route string (exclude the leading /)
        public ViewResult Result(User newUser)
        {
            return View(newUser);
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
