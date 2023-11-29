using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNdishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNdishes.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {   
        ViewBag.AllChefs=_context.Chefs.Include(a=>a.ListOfDishes).ToList();

        return View();
    }
    [HttpGet("chefs/new")]
    public IActionResult ChefForm()
    {
        return View("AddChef");
    }
    [HttpPost("chefs/new")]
    public IActionResult AddChef(Chef newChef){
        if(ModelState.IsValid){
            _context.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else{
            return View("AddChef");
        }
    }
    [HttpGet("dishes/new")]
    public IActionResult DishForm(){
        ViewBag.AllChefs=_context.Chefs.ToList();
        return View("AddDish");
    }

    [HttpPost("dishes/new")]
    public IActionResult AddDish(Dish newDish){
        ViewBag.AllChefs=_context.Chefs.ToList();
        if(ModelState.IsValid){
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else{
            return View("AddDish");
        }
    }
    [HttpGet("/dishes")]
    public IActionResult Dishes(){
        ViewBag.AllDishes=_context.Dishes.Include(a=>a.Chef).ToList();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
