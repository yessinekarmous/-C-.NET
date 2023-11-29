using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginAndRegistration.Controllers;

public class HomeController : Controller
{   private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _context=context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost("")]
    public IActionResult Form(User NewUser){
        if(ModelState.IsValid){

            if(_context.Users.Any(a=>a.Email==NewUser.Email))
            {
                ModelState.AddModelError("Email" ,"Email already in use");
                return View("Index");
            }
            _context.Add(NewUser);
            PasswordHasher<User> hasher=new PasswordHasher<User>();
            NewUser.Password= hasher.HashPassword(NewUser,NewUser.Password);
            _context.SaveChanges();
            return RedirectToAction("Success");               
                }
        else{
            return View("Index");
        }
    }
    [HttpGet("Success")]
    public IActionResult Success(){
        return View();
    }
    [HttpPost("/users/login")]
    public IActionResult Login(LogUser NewLogUser){
        if(ModelState.IsValid){
            User UserfromDb=_context.Users.FirstOrDefault(a=>a.Email==NewLogUser.LogEmail);
            if(UserfromDb==null){
                ModelState.AddModelError("LogEmail","No User Found");
                return View("Index");
            }
            PasswordHasher<LogUser> hasher=new PasswordHasher<LogUser>();
            var result= hasher.VerifyHashedPassword(NewLogUser,UserfromDb.Password,NewLogUser.LogPassword);
            if(result==0){
                ModelState.AddModelError("Email" ,"Email already in use");
                return View("Index");
            }
            else{
                return RedirectToAction("Success");
            }
            
        }
        else{
            return View("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
