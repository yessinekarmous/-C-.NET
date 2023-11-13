using Microsoft.AspNetCore.Mvc;
namespace TimeDisplay.Controllers;     //be sure to use your own project's namespace!
    public class TimeController : Controller   //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet]       //type of request
        [Route("")]     //associated route string (exclude the leading /)
        public ViewResult  Index()
        {
            DateTime CurrentTime = DateTime.Now;
            ViewBag.Date=CurrentTime.ToString("yyyy-MMM-dd ");
            

            return View();
        }
    }

