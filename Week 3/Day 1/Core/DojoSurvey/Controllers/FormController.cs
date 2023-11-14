using Microsoft.AspNetCore.Mvc;
namespace DojoSurvey.Controllers;    //be sure to use your own project's namespace!
    public class FormController : Controller   //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet]       //type of request
        [Route("")]     //associated route string (exclude the leading /)
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]       //type of request
        [Route("result")]     //associated route string (exclude the leading /)
        public IActionResult Process(string name,string language ,string location,string comment)
        {
            ViewBag.name=name;
            ViewBag.language=language;
            ViewBag.location=location;
            ViewBag.comment=comment;
            return View("Result");
        }

        [HttpGet]       //type of request
        [Route("result")]     //associated route string (exclude the leading /)
        public ViewResult Result()
        {
            return View();
        }
    }

