using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ViewModelFun.Models;

// using Microsoft.AspNetCore.Http;
namespace ViewModelFun.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        string message="Hello this the string passed as a model ";
        return View("Index",message);
    }

    [HttpGet("numbers")]
    public IActionResult Numbers()
    {
        int[] myArray = new int[] {1,2,3,10,43,5};
        return View(myArray);
    }
     [HttpGet("user")]
    public IActionResult OneUser()
    {     
        User OneUser=new User()
    {
        FirstName="Moose",
        LastName="Philips"
    };
        return View(OneUser);
    }


    [HttpGet("users")]
    public IActionResult Users()
    {
        List<User> Users=new List<User>();
        
        User User1 = new User
        {
        FirstName = "Moose",
        LastName = "Philipd"
        };
        User User2 = new User
        {
        FirstName = "Rene",
        LastName = "Ricky"
        };
        User User3 = new User
        {
        FirstName = "Sarah",
        };

        Users.Add(User1);
        Users.Add(User2);
        Users.Add(User3);
        
        return View(Users);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
