using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegistration.Models;
using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Http;
// using System.Security.Claims; 
// using System.Threading;
namespace LoginAndRegistration.Controllers;
// using System;
// using System.Security.Principal;
// using Microsoft.AspNetCore.Http;



public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;
    // private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _context = context;
        _logger = logger;
        // _httpContextAccessor = httpContextAccessor;
    }
    // private void SetPrincipal(IPrincipal principal)
    // {
    //     if (principal is ClaimsPrincipal claimsPrincipal)
    //     {
    //         Thread.CurrentPrincipal = claimsPrincipal;
    //         if (_httpContextAccessor.HttpContext != null)
    //         {
    //             _httpContextAccessor.HttpContext.User = claimsPrincipal;
    //         }
    //     }
    // }
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost("")]
    public IActionResult Form(User NewUser)
    {
        if (ModelState.IsValid)
        {

            if (_context.Users.Any(a => a.Email == NewUser.Email))
            {
                ModelState.AddModelError("Email", "Email already in use");
                return View("Index");
            }else{
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                NewUser.Password = hasher.HashPassword(NewUser, NewUser.Password);
                _context.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("userId",NewUser.UserId);
                return RedirectToAction("Success"); 
            }
            
        }
        else
        {
            return View("Index");
        }
    }
    [HttpGet("Success")]
    public IActionResult Success()
    {
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }else{
            return View("Success");
        }  
    }
    [HttpGet("logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpPost("/users/login")]
    public IActionResult Login(LogUser NewLogUser)
    {
        if (ModelState.IsValid)
        {
            User UserfromDb = _context.Users.FirstOrDefault(a => a.Email == NewLogUser.LogEmail);
            if (UserfromDb == null)
            {
                ModelState.AddModelError("LogEmail", "No User Found");
                return View("Index");
            }
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(NewLogUser, UserfromDb.Password, NewLogUser.LogPassword);
            if (result == 0)
            {
                ModelState.AddModelError("Email", "Email already in use");
                return View("Index");
            }
            else
            {
                HttpContext.Session.SetInt32("userId",UserfromDb.UserId);
                return RedirectToAction("Success");
            }

        }
        else
        {
            return View("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
