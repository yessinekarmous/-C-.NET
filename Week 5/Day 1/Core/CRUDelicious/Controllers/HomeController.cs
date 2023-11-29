using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.Dishes=_context.Dishes.OrderBy(a=>a.CreatedAt);
        return View();
    }

    [HttpGet("dishes/new")]
    public IActionResult Add(){
        return View();
    }
    [HttpPost("")]
    public IActionResult Form(Dish newDish){
        if (ModelState.IsValid){
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else{
            return View("Add");
        }
    }
    [HttpGet("dishes/{ItemId}")]
    public IActionResult ShowOne(int ItemId){
        Dish OneDish=_context.Dishes.FirstOrDefault(a=>a.ItemId==ItemId);
        return View(OneDish);
    }

    [HttpGet("Delete/{ItemId}")]
    public IActionResult Delete(int ItemId){
        Dish DishToDelete=_context.Dishes.FirstOrDefault(a=>a.ItemId==ItemId);
        _context.Remove(DishToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }


    [HttpGet("dishes/{ItemId}/edit")]
    public IActionResult Update(int ItemId)
    {
        Dish OneDish=_context.Dishes.SingleOrDefault(a=>a.ItemId==ItemId);
        
        return View("Edit",OneDish);
    }
    [HttpPost("dishes/{ItemId}/edit")]
    public IActionResult EditForm(int ItemId,Dish UpdatedDish)
    {
        Dish OldDish=_context.Dishes.FirstOrDefault(a=>a.ItemId==ItemId);

        // _context.Dishes.FirstOrDefault(a=>a.ItemId==ItemId).DishName=UpdatedDish.DishName;
        if(ModelState.IsValid){
        OldDish.DishName=UpdatedDish.DishName;
        OldDish.ChefName=UpdatedDish.ChefName;
        OldDish.Calories=UpdatedDish.Calories;
        OldDish.Description=UpdatedDish.Description;
        OldDish.Tastiness=UpdatedDish.Tastiness;
        OldDish.UpdatedAt=UpdatedDish.UpdatedAt;
        _context.SaveChanges();
        return RedirectToAction("Index");
        }
        ViewBag.Name=OldDish.DishName;
        return View("Edit",UpdatedDish);
        
        
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
