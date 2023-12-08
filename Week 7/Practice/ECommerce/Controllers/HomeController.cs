using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers;

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
        ViewBag.Products=_context.Products.Take(5).ToList();
        ViewBag.Orders=_context.Orders.Include(p=>p.product).Include(c=>c.customer).Take(3).ToList();
        ViewBag.Customers=_context.Customers.Take(3).ToList();
        return View();
    }
    [HttpGet("orders")]
    public IActionResult Orders(){
        ViewBag.AllProducts=_context.Products.ToList();
        ViewBag.AllCustomers=_context.Customers.ToList();
        ViewBag.AllOrders=_context.Orders.Include(p=>p.product).Include(c=>c.customer).ToList();
        return View();
    }
    [HttpGet("products")]
    public IActionResult Products(){
        ViewBag.AllProducts=_context.Products.ToList();
        return View();
    }
    [HttpGet("customers")]
    public IActionResult Customers(){
        ViewBag.AllCustomers=_context.Customers.ToList();
        return View();
    }
    [HttpPost]
    public IActionResult AddCustomer(Customer newCustomer){
        if(ModelState.IsValid){
            _context.Add(newCustomer);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }
        ViewBag.AllCustomers=_context.Customers.ToList();
        return View("Customers");
    }
    [HttpGet("delete/{id}")]
    public IActionResult DeleteCustomer(int id){
        Customer customerToDelete=_context.Customers.FirstOrDefault(c=>c.CustomerId==id);
        _context.Remove(customerToDelete);
        _context.SaveChanges();
        return RedirectToAction("Customers");

    }
    [HttpPost]
    public IActionResult AddProduct(Product newProduct){
        if(ModelState.IsValid){
            _context.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }
        ViewBag.AllProducts=_context.Products.ToList(); 
        return View("Products");
    }

    [HttpPost]
    public IActionResult AddOrder(Order newOrder){
        Product ProductOrdered=_context.Products.FirstOrDefault(p=>p.ProductId==newOrder.ProductId);
        if(newOrder.Quantity>ProductOrdered.ProductQuantity){
            ModelState.AddModelError("Quantity","We have no more Products");
        }
        if(ModelState.IsValid){
            _context.Add(newOrder);
            ProductOrdered.ProductQuantity-=newOrder.Quantity;
            _context.SaveChanges();
            return RedirectToAction("Orders");
        }
        ViewBag.AllProducts=_context.Products.ToList();
        ViewBag.AllCustomers=_context.Customers.ToList();
        ViewBag.AllOrders=_context.Orders.Include(p=>p.product).Include(c=>c.customer).ToList();
        return View("Orders");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
