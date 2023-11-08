using Microsoft.AspNetCore.Mvc;
namespace RazorFun.Controllers;     //be sure to use your own project's namespace!
    public class RazorController : Controller   //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet]       //type of request
        [Route("")]     //associated route string (exclude the leading /)
        public ViewResult Index()
        {
            return View();
        }
    }

