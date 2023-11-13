using Microsoft.AspNetCore.Mvc;
namespace Portfolio2.Controllers;     //be sure to use your own project's namespace!
    public class PortfolioController : Controller   //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet]       //type of request
        [Route("")]     //associated route string (exclude the leading /)
        public ViewResult Index()
        {
            return View();
        }


        [HttpGet]       //type of request
        [Route("projects")]     //associated route string (exclude the leading /)
        public ViewResult Projects()
        {
            return View();
        }
        [HttpGet]       //type of request
        [Route("contact")]     //associated route string (exclude the leading /)
        public ViewResult Contact()
        {
            return View();
        }

    }

