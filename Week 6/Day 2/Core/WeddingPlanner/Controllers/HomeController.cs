using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace WeddingPlanner.Controllers;

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
            return RedirectToAction("Weddings");
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
            return RedirectToAction("Weddings");
        }
        return View("Index");
    }
    [HttpGet("logout")]
    public IActionResult Logout (){
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpGet("weddings")]
    public IActionResult Weddings(){
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }
        List<Wedding> AllWeddings=_context.Weddings.Include(w=>w.Guests).ThenInclude(a=>a.User).Include(m=>m.Creator).ToList();
        foreach(Wedding one in AllWeddings){
            if(one.Date<DateTime.Now){
                _context.Remove(one);
                _context.SaveChanges();
            }
        }
        ViewBag.AllWeddings=AllWeddings;
        ViewBag.AllAtendances=_context.Attendances.ToList();
        return View();
    }
    [HttpGet("weddings/new")]
    public IActionResult WeddingForm(){
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }
        return View();
    }
    [HttpPost]
    public IActionResult AddWedding(Wedding newWedding){
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }
        if(ModelState.IsValid){
            _context.Add(newWedding);
            _context.SaveChanges();
            return RedirectToAction("Weddings");
        }
        return View("WeddingForm");
    }
    [HttpPost]
    public IActionResult Attendance(Attendance newAttendance){
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }
        _context.Add(newAttendance);
        _context.SaveChanges();
        return RedirectToAction("Weddings");
    }
    [HttpGet("deleteAttendance/{id}")]
    public IActionResult DeleteAttendance(int id){
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }
        Attendance attendanceToDelete=_context.Attendances.FirstOrDefault(a=>a.AttendanceId==id);
        _context.Remove(attendanceToDelete);
        _context.SaveChanges();
        return RedirectToAction("Weddings");
    }
    [HttpGet("delete/{id}")]
    public IActionResult deleteWedding(int id){
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }
        Wedding weddingToDelete=_context.Weddings.FirstOrDefault(w=>w.WeddingId==id);
        _context.Remove(weddingToDelete);
        _context.SaveChanges();
        return RedirectToAction("Weddings");
    }
    [HttpGet("weddings/{id}")]
    public IActionResult OneWedding(int id){
        if(HttpContext.Session.GetInt32("userId")==null){
            return RedirectToAction("Index");
        }
        Wedding oneWedding=_context.Weddings.Include(a=>a.Guests).ThenInclude(q=>q.User).FirstOrDefault(w=>w.WeddingId==id);
        return View(oneWedding);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
