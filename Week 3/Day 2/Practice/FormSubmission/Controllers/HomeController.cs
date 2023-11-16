using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormSubmission.Models;
using System;
namespace FormSubmission.Controllers;

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
    [HttpPost("")]
    public IActionResult Process(User newOne)
    {
        if(ModelState.IsValid){
            return RedirectToAction("Success");
        }
        else{
            return View("Index");
        }
    }

    

    [HttpGet("Success")]
    public IActionResult Success()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
