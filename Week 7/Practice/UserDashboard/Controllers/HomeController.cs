using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserDashboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserDashboard.Controllers;

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
        ViewBag.LoggedIn=false;
        return View();
    }
    [HttpGet("signin")]
    public IActionResult SignIn(){
        ViewBag.LoggedIn=false;
        return View();
    }
    [HttpGet(template: "register")]
    public IActionResult SignUp(){
        ViewBag.LoggedIn=false;
        return View();
    }
    [HttpPost]
    public IActionResult Register(User newUser){
        bool isNotFirstUser=_context.Users.Any();
        ViewBag.LoggedIn=false;
        if(ModelState.IsValid){
            if(_context.Users.Any(u=>u.Email==newUser.Email)){
                ModelState.AddModelError("Email","Email already used .");
                return View("Index");
            }
            if(!isNotFirstUser){
                newUser.UserLevel="admin";
            }
            if(isNotFirstUser){
                newUser.UserLevel="user";}


            PasswordHasher<User> hasher=new PasswordHasher<User>();
            newUser.Password=hasher.HashPassword(newUser , newUser.Password );
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId",newUser.UserId);
            HttpContext.Session.SetString("role",newUser.UserLevel);
            if(newUser.UserLevel=="admin"){
                return RedirectToAction("DashboardA");
            }
            if(newUser.UserLevel=="user"){
                return RedirectToAction("DashboardU");
            }
            return RedirectToAction("DashboardA");
        }else {
        return View("SignUp");
        }
    }
    [HttpPost]
    public IActionResult Login(LogUser newLogUser){
        ViewBag.LoggedIn=false;
        if(ModelState.IsValid){
            User userfromdb= _context.Users.FirstOrDefault(u=>u.Email==newLogUser.LogEmail);
            if(userfromdb==null){
                ModelState.AddModelError("LogEmail","Invalid Login Attempt");
                return View("SignIn");
            }
            PasswordHasher<LogUser> hasher=new PasswordHasher<LogUser>();
            var result=hasher.VerifyHashedPassword(newLogUser,userfromdb.Password,newLogUser.LogPassword );
            if(result==0){
                ModelState.AddModelError("LogEmail","Invalid Login Attempt");
                return View("SignIn");
            }
            HttpContext.Session.SetInt32("userId",userfromdb.UserId);
            HttpContext.Session.SetString("role",userfromdb.UserLevel);
            if(userfromdb.UserLevel=="admin"){
                return RedirectToAction("DashboardA");
            }
            if(userfromdb.UserLevel=="user"){
                return RedirectToAction("DashboardU");
            }
            
        }
        return View("SignIn");
    }
    [HttpGet("logout")]
    public IActionResult Logout (){
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpGet("dashboard/admin")]
    public IActionResult DashboardA(){
        ViewBag.LoggedIn=true;
        ViewBag.AllUsers=_context.Users.ToList();
        return View();
    }
    [HttpGet("dashboard")]
    public IActionResult DashboardU()
    {
        ViewBag.LoggedIn=true;
        ViewBag.AllUsers=_context.Users.ToList();
        return View();
    }
    [HttpGet("/add")]
    public IActionResult AddUser(){
        ViewBag.LoggedIn=true;
        return View();
    }
    [HttpGet("profile")]
    public IActionResult Profile(){
        User usertoEdit=_context.Users.SingleOrDefault(u=>u.UserId==HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedIn=true;
        return View(usertoEdit);
    }
    [HttpPost]
    public IActionResult updateProfile(User usertoEdit){
        User userFromDB=_context.Users.FirstOrDefault(u=>u.UserId==HttpContext.Session.GetInt32("userId"));
        if(ModelState.IsValid){
            userFromDB.FName=usertoEdit.FName;
            userFromDB.LName=usertoEdit.LName;
            userFromDB.Email=usertoEdit.Email;
            userFromDB.Password=userFromDB.Password;
            userFromDB.ConfirmPassword=userFromDB.ConfirmPassword;
            userFromDB.Description=usertoEdit.Description;
            userFromDB.UserLevel=userFromDB.UserLevel;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
        ViewBag.LoggedIn=true;
        return View("Profile");
    }
    [HttpGet("/users/show/{id}")]
    public IActionResult ShowOne(int id){
        ViewBag.LoggedIn=true;
        User UserToShow=_context.Users.FirstOrDefault(u=>u.UserId==id);
        return View(UserToShow);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
