using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DojoSurveyMo.Models;

namespace DojoSurveyMo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
        {
            
            return View();
        }
    
    [HttpPost]       
    [Route("result")]    
    public IActionResult Process(Survey NewSurvey)
        {
            
            return View("Result",NewSurvey);
        }
    
    [HttpGet]     
    [Route("result")]    
        public ViewResult Result(Survey NewSurvey)
        {
            return View(NewSurvey);
        }    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
