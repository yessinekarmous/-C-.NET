using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankAccounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace BankAccounts.Controllers;

public class HomeController : Controller
{
      private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context=context;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(User newUser)
    {
        if(ModelState.IsValid){
            if(_context.Users.Any(u=>u.Email==newUser.Email)){
                ModelState.AddModelError("Email","Email already used !");
                return View("Index");
            }
            PasswordHasher<User> hasher=new PasswordHasher<User>();
            newUser.Password=hasher.HashPassword(newUser,newUser.Password);
            _context.Users.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId",newUser.UserId);
            HttpContext.Session.SetString("username",newUser.FName+" "+newUser.LName);
            return RedirectToAction("Account",newUser.UserId);
        }
        
        return View("Index");
    }
    [HttpPost]
    public IActionResult Login(LogUser newLogUser){
        if(ModelState.IsValid){
            User userfromdb=_context.Users.FirstOrDefault(a=>a.Email==newLogUser.LogEmail);
            if(userfromdb==null){
                ModelState.AddModelError("LogEmail","Invalid Login Attempt");
                return View("Index");
            }
            PasswordHasher<LogUser> hasher= new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(newLogUser,userfromdb.Password,newLogUser.LogPassword);
            if(result==0){
                ModelState.AddModelError("LogEmail","Invalid Login Attempt");
                return View("Index");
            }
            HttpContext.Session.SetInt32("userId",userfromdb.UserId);
            HttpContext.Session.SetString("username",userfromdb.FName+" "+userfromdb.LName);
            return RedirectToAction("Account",new { id = userfromdb.UserId });
        }
        return View("Index");
    }

    [HttpGet("accounts/{id}")]
    public IActionResult Account(int id){
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }
        else if(HttpContext.Session.GetInt32("userId")!=id)
        {
            return RedirectToAction("Account",new{id=HttpContext.Session.GetInt32("userId") });
        }
        User? myUser=_context.Users.Include(a=>a.UserTransactions).FirstOrDefault(u=>u.UserId==id);
        double s=0;
        foreach(Transaction one in myUser.UserTransactions){
            s=s+one.Amount ;
        }
        ViewBag.AllTransactions=myUser.UserTransactions;
        ViewBag.CBalance=s;
        return View();
    }
    [HttpPost()]
    public IActionResult myForm(Transaction newTransaction){
        int id=(int)HttpContext.Session.GetInt32("userId");
        User? myUser=_context.Users.Include(a=>a.UserTransactions).FirstOrDefault(u=>u.UserId==id);
        double s=0;
        foreach(Transaction one in myUser.UserTransactions){
            s=s+one.Amount ;
        }
        ViewBag.AllTransactions=myUser.UserTransactions;
        ViewBag.CBalance=s;
        if(newTransaction.Amount<0 && newTransaction.Amount+s<0){
            ModelState.AddModelError("Amount","No more");
            // the problem is here
            return View("Account",newTransaction);
        }
        if(ModelState.IsValid){
        _context.Add(newTransaction);
        _context.SaveChanges();
        return RedirectToAction("Account",new{id});
        }
        // the problem is here
        return View("Account",newTransaction);

    }




    [HttpGet("logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
