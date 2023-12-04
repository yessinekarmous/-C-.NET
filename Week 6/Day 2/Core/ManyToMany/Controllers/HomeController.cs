using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ManyToMany.Models;
using Microsoft.EntityFrameworkCore;

namespace ManyToMany.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,MyContext context)
    {
        _context=context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.AllProducts=_context.Products.ToList();
        return View();
    }
    [HttpPost]
    public IActionResult AddProduct(Product newProduct)
    {
        if(ModelState.IsValid){
            _context.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.AllProducts=_context.Products.ToList();
        return View("Index");
    }
    [HttpGet("products/{id}")]
    public IActionResult OneProduct(int id){
        ViewBag.OneProduct =_context.Products.Include(p=>p.myCategories).ThenInclude(o=>o.Category).FirstOrDefault(a=>a.ProductId==id);
        ViewBag.AllCategories=_context.Categories.ToList();
        return View();
    }
    [HttpGet("categories/{id}")]
    public IActionResult OneCategory(int id){
        ViewBag.OneCategory =_context.Categories.Include(p=>p.myProducts).ThenInclude(o=>o.Product).FirstOrDefault(a=>a.CategoryId==id);
        ViewBag.AllProducts=_context.Products.ToList();
        return View();
    }

    [HttpGet("categories")]
    public IActionResult Categories(){
        ViewBag.AllCategories=_context.Categories.ToList();
        return View();
    }
    [HttpPost]
    public IActionResult AddCategory(Category newCategory)
    {
        if(ModelState.IsValid){
            _context.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }
        ViewBag.AllCategories=_context.Categories.ToList();
        return View("Categories");
    }

    [HttpPost]
    public IActionResult AssociationCateg(Association newAssociation){
        _context.Associations.Add(newAssociation);
        _context.SaveChanges();
        return RedirectToAction("OneProduct",new{id=newAssociation.ProductId});
    }
    [HttpPost]
    public IActionResult AssociationProduct(Association newAssociation){
        _context.Associations.Add(newAssociation);
        _context.SaveChanges();
        return RedirectToAction("OneCategory",new{id=newAssociation.CategoryId});
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
