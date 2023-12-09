using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheWall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Controllers;

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
        return View();
    }
    [HttpPost]
    public IActionResult Register(User newUser){
        if(ModelState.IsValid){
            if(_context.Users.Any(u=>u.Email==newUser.Email)){
                ModelState.AddModelError("Email","Email already used .");
                return View("Index");
            }
            PasswordHasher<User> hasher=new PasswordHasher<User>();
            newUser.Password=hasher.HashPassword(newUser , newUser.Password );
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId",newUser.UserId);
            HttpContext.Session.SetString("name",newUser.FName);
            return RedirectToAction("PostPage");
        }
        return View("Index");
    }
    [HttpPost]
    public IActionResult Login(LogUser newLogUser){
        if(ModelState.IsValid){
            User userfromdb= _context.Users.FirstOrDefault(u=>u.Email==newLogUser.LogEmail);
            if(userfromdb==null){
                ModelState.AddModelError("LogEmail","Invalid Login Attempt");
                return View("Index");
            }
            PasswordHasher<LogUser> hasher=new PasswordHasher<LogUser>();
            var result=hasher.VerifyHashedPassword(newLogUser,userfromdb.Password,newLogUser.LogPassword );
            if(result==0){
                ModelState.AddModelError("LogEmail","Invalid Login Attempt");
                return View("Index");
            }
            HttpContext.Session.SetInt32("userId",userfromdb.UserId);
            HttpContext.Session.SetString("name",userfromdb.FName);
            return RedirectToAction("PostPage");
        }
        return View("Index");
    }

    [HttpGet("logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpGet("/messages")]
    public IActionResult PostPage(){
        ViewBag.AllMessages=_context.Messages.Include(u=>u.Poster).OrderBy(m=>m.CreatedAt).ToList();
        ViewBag.AllComments=_context.Comments.Include(u=>u.Commentator).OrderBy(m=>m.CreatedAt).ToList();
        return View();
    }

    [HttpPost]
    public IActionResult PostMessage(Message newMessage){
        if(ModelState.IsValid){
            _context.Add(newMessage);
            _context.SaveChanges();
            return RedirectToAction("PostPage");
        }
        ViewBag.AllMessages=_context.Messages.Include(u=>u.Poster).OrderBy(m=>m.CreatedAt).ToList();
        ViewBag.AllComments=_context.Comments.Include(u=>u.Commentator).OrderBy(m=>m.CreatedAt).ToList();
        return View("PostPage");
    }
    
    [HttpPost]
    public IActionResult PostComment(Comment newComment){
        if(ModelState.IsValid){
            newComment.UserId=(int)HttpContext.Session.GetInt32("userId");
            _context.Add(newComment);
            _context.SaveChanges();
            return RedirectToAction("PostPage");
        }
        ViewBag.AllMessages=_context.Messages.Include(u=>u.Poster).OrderBy(m=>m.CreatedAt).ToList();
        ViewBag.AllComments=_context.Comments.Include(u=>u.Commentator).OrderBy(m=>m.CreatedAt).ToList();
        return View("PostPage");
    }
    [HttpGet("delete/{id}")]
    public IActionResult DeleteMessage(int id){
        Message msgToRemove=_context.Messages.FirstOrDefault(m=>m.MessageId==id);
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("PostPage");
        }
        if(msgToRemove.UserId!=HttpContext.Session.GetInt32("userId")){
            return RedirectToAction("Logout");
        }
        _context.Remove(msgToRemove);
        _context.SaveChanges();
        return RedirectToAction("PostPage");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
