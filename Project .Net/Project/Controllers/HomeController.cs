using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Microsoft.AspNetCore.Http;
namespace Project.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _context = context;
        _logger = logger;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        HttpContext.Session.Clear();
        return View();

    }

    [HttpGet("login")]

    public IActionResult Login()
    {
        HttpContext.Session.Clear();
        return View();
    }

    [HttpPost("")]
    public IActionResult Register(User newUser)
    {
        if (ModelState.IsValid)
        {
            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "Email already used .");
                return View("Index");
            }
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", newUser.UserId);
            HttpContext.Session.SetString("userRole", newUser.UserRole);
            if (newUser.UserRole == "user")
            {
                return RedirectToAction("book");
            }
            if (newUser.UserRole == "admin")
            {
                return RedirectToAction("LandingPageAdmin");
            }
        }
        return View("Index");


    }
    [HttpPost("login")]
    public IActionResult Login(LogUser newLogUser)
    {
        if (ModelState.IsValid)
        {
            User userfromdb = _context.Users.FirstOrDefault(u => u.Email == newLogUser.LogEmail);
            if (userfromdb == null)
            {
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Login");
            }
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(newLogUser, userfromdb.Password, newLogUser.LogPassword);
            if (result == 0)
            {
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Login");
            }
            HttpContext.Session.SetInt32("userId", userfromdb.UserId);
            HttpContext.Session.SetString("userRole", userfromdb.UserRole);
            if (userfromdb.UserRole == "user")
            {
                return RedirectToAction("book");
            }
            if (userfromdb.UserRole == "admin")
            {
                return RedirectToAction("LandingPageAdmin");
            }

        }
        return View("Login");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpGet("MyCalendar")]
    public IActionResult book(DatePick newDate)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "user"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.AllAppointment = _context.Appointments.Where(a => a.Time.Date == newDate.TheDate).ToList();
        return View();
    }
    [HttpPost("MyCalendar")]
    public IActionResult GetDate(DatePick newDate)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "user"))
        {
            return RedirectToAction("Index");
        }
        if (ModelState.IsValid)
        {
            ViewBag.AllAppointment = _context.Appointments.Where(a => a.Time.Date == newDate.TheDate).ToList();
            ViewBag.ShowDate = newDate.TheDate.Date;
            return View("book", newDate);
        }
        return View("book", newDate);
    }
    [HttpPost()]
    public IActionResult BookingAction(DateTime TimePicked, DateTime DatePicked)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "user"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.ShowDate = DatePicked.Date;
        DateTime FinalDate = new DateTime(DatePicked.Year, DatePicked.Month, DatePicked.Day, TimePicked.Hour, TimePicked.Minute, 0);
        Appointment appointmentToAdd = new Appointment
        {
            UserId = (int)HttpContext.Session.GetInt32("userId"),
            Time = FinalDate
        };
        _context.Appointments.Add(appointmentToAdd);
        _context.SaveChanges();

        return RedirectToAction("Ahistory");
    }

    [HttpGet(template: "history")]
    public IActionResult Ahistory()
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "user"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.MyAppointmentsPast = _context.Appointments.Where(a => a.UserId == (HttpContext.Session.GetInt32("userId")) && a.Time < DateTime.Now).OrderBy(a => a.Time.Date).ToList();
        ViewBag.MyAppointmentsFutur = _context.Appointments.Where(a => a.UserId == (HttpContext.Session.GetInt32("userId")) && a.Time > DateTime.Now).OrderBy(a => a.Time.Date).ToList();
        return View();
    }
    [HttpGet(template: "evaluations")]
    public IActionResult Evaluations()
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "user"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.AllComments = _context.Evaluations.ToList();
        return View();
    }
    [HttpPost("evaluations")]
    public IActionResult AddEvaluation(Evaluation newOne)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "user"))
        {
            return RedirectToAction("Index");
        }
        if (ModelState.IsValid)
        {
            newOne.UserId = (int)HttpContext.Session.GetInt32("userId");
            _context.Evaluations.Add(newOne);
            _context.SaveChanges();
            return RedirectToAction("Evaluations");
        }
        ViewBag.AllComments = _context.Evaluations.ToList();
        return View("Evaluations");
    }
    [HttpGet("deleteComment/{id}")]
    public IActionResult DeleteEvaluation(int id)
    {
        Evaluation evaluationToRemove = _context.Evaluations.FirstOrDefault(m => m.EvaluationId == id);
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("Index");
        }
        if ((evaluationToRemove.UserId != HttpContext.Session.GetInt32("userId")) && (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Logout");
        }
        _context.Remove(evaluationToRemove);
        _context.SaveChanges();
        if (HttpContext.Session.GetString("userRole") == "user")
        {
            return RedirectToAction("Evaluations");
        }
        if (HttpContext.Session.GetString("userRole") == "admin")
        {
            return RedirectToAction("AdminEvaluations");
        }
        return View("NotFound");
    }


    ///////////////////////////////////////////admin Controller////////////////////////////////

    [HttpGet("admin/calendar")]
    public IActionResult LandingPageAdmin(DatePickAdmin newDate)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.AllAppointment = _context.Appointments.Where(a => a.Time.Date == newDate.TheDate).ToList();
        ViewBag.DateForm = newDate.TheDate;
        return View();
    }
    [HttpPost("admin/calendar")]
    public IActionResult GetDateAdmin(DatePickAdmin newDate)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        if (ModelState.IsValid)
        {
            ViewBag.AllAppointment = _context.Appointments.Include(a => a.Client).Where(a => a.Time.Date == newDate.TheDate).ToList();
            ViewBag.ShowDate = newDate.TheDate.Date;
            return View("LandingPageAdmin", newDate);
        }
        return View("LandingPageAdmin", newDate);
    }

    [HttpGet("admin/appointment/{id}")]
    public IActionResult AdminShowOne(int id)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        Appointment AppointmentToShow = _context.Appointments.Include(a => a.Client).FirstOrDefault(u => u.AppointmentId == id);
        return View(AppointmentToShow);
    }
    [HttpGet("attendance/{value}/{id}")]
    public IActionResult UpdateAttendance(string value, int id)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        Appointment appointmentToUpdate = _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        if (value == "absent")
        {
            appointmentToUpdate.Attendance = "present";
            _context.SaveChanges();
            return RedirectToAction("AdminShowOne", new { id });
        }
        else if (value == "present")
        {
            appointmentToUpdate.Attendance = "absent";
            _context.SaveChanges();
            return RedirectToAction("AdminShowOne", new { id });
        }
        return RedirectToAction("AdminShowOne", new { id });
    }
    [HttpPost("admin/appointment/{id}")]
    public IActionResult updateTime(Appointment newAppointment, int id)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        Appointment appointmentToUpdate = _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        if (ModelState.IsValid)
        {
            appointmentToUpdate.Time = newAppointment.Time;
            appointmentToUpdate.Attendance = newAppointment.Attendance;
            appointmentToUpdate.UpdatedAt = newAppointment.UpdatedAt;
            _context.SaveChanges();
            return RedirectToAction("AdminShowOne", new { id });
        }
        return View("AdminShowOne", appointmentToUpdate);
    }
    [HttpGet("deleteapp/{id}")]
    public IActionResult deleteApp(int id)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        Appointment appointmentToDelete = _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        _context.Appointments.Remove(appointmentToDelete);
        _context.SaveChanges();
        return RedirectToAction("LandingPageAdmin");
    }

    [HttpGet("admin/evaluations")]
    public IActionResult AdminEvaluations()
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.AllEvaluations = _context.Evaluations.Include(a => a.Creator).ToList();
        return View();
    }
    [HttpGet("admin/user/{id}")]
    public IActionResult AdminOneUser(int id)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.MyAppointmentsPast = _context.Appointments.Include(u => u.Client).Where(a => a.UserId == id && a.Time < DateTime.Now).OrderBy(a => a.Time.Date).ToList();
        ViewBag.MyAppointmentsFutur = _context.Appointments.Include(u => u.Client).Where(a => a.UserId == id && a.Time > DateTime.Now).OrderBy(a => a.Time.Date).ToList();
        User UserToShow = _context.Users.FirstOrDefault(u => u.UserId == id);
        return View(UserToShow);
    }
    [HttpGet("admin/allUsers")]
    public IActionResult AdminAllUsers(int id)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.AllUsers = _context.Users.Where(u => u.UserRole == "user").ToList();
        return View();
    }
    [HttpGet("deleteUser/{id}")]
    public IActionResult DeleteUser(int id)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        User userToDelete = _context.Users.FirstOrDefault(u => u.UserId == id);
        _context.Users.Remove(userToDelete);
        _context.SaveChanges();
        return RedirectToAction("AdminAllUsers");
    }
    [HttpGet("admin/create")]
    public IActionResult AdminAddApp(int id)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        ViewBag.AllUsers = _context.Users.Where(u => u.UserRole == "user").ToList();
        return View();
    }
    [HttpPost("admin/create")]
    public IActionResult AddAppointment(Appointment newAppointment)
    {
        if ((HttpContext.Session.GetInt32("userId") == null) || (HttpContext.Session.GetString("userRole") != "admin"))
        {
            return RedirectToAction("Index");
        }
        if (_context.Appointments.Any(a => a.Time == newAppointment.Time))
        {
            ViewBag.AllUsers = _context.Users.Where(u => u.UserRole == "user").ToList();
            ModelState.AddModelError("Time", "Appointment already booked or locked.");
            return View("AdminAddApp");
        }
        List<DateTime> allowedTimes = new List<DateTime>
{
    newAppointment.Time.Date.Add(new TimeSpan(8, 0, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(8, 30, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(9, 0, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(9, 30, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(10, 0, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(10,  30, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(11, 00, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(11, 30, 0)),

    newAppointment.Time.Date.Add(new TimeSpan(14, 00, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(14, 30, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(15, 00, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(15, 30, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(16, 00, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(16, 30, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(17, 00, 0)),
    newAppointment.Time.Date.Add(new TimeSpan(17, 30, 0)),

};

        if (!allowedTimes.Contains(newAppointment.Time))
        {
            ViewBag.AllUsers = _context.Users.Where(u => u.UserRole == "user").ToList();
            ModelState.AddModelError("Time", "Invalid appointment time. Please choose a valid time.");
            return View("AdminAddApp");
        }

        if (ModelState.IsValid)
        {
            _context.Appointments.Add(newAppointment);
            _context.SaveChanges();
            ViewBag.AllUsers = _context.Users.Where(u => u.UserRole == "user").ToList();
            return RedirectToAction("LandingPageAdmin");
        }
        ViewBag.AllUsers = _context.Users.Where(u => u.UserRole == "user").ToList();
        return View("AdminAddApp");
    }







    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
