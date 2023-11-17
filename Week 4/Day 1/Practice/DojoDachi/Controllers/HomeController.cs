using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DojoDachi.Models;
namespace DojoDachi.Controllers;
using Microsoft.AspNetCore.Http;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet("DojoDachi")]
    public IActionResult Index()
    {
        HttpContext.Session.SetInt32("fullness",20);
        HttpContext.Session.SetInt32("happiness",20);
        HttpContext.Session.SetInt32("energy",50); 
        HttpContext.Session.SetInt32("meals",3);
        HttpContext.Session.SetString("phrase","Hello");
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
