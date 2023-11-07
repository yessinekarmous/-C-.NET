using Microsoft.AspNetCore.Mvc;
namespace PORTFOLIO1.Controllers;     //be sure to use your own project's namespace!
    public class FirstController : Controller   //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet]       //type of request
        [Route("")]     //associated route string (exclude the leading /)
        public string Index()
        {
            return "This is my Index!";
        }

        [HttpGet]      
        [Route("projects")]   
        public string Projects()
        {
            return "These are my Projects!";
        }

        [HttpGet]       
        [Route("contact")]     
        public string Contact()
        {
            return "These are my Contact!";
        }
    }

