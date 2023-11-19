using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DojoDachi.Models;
namespace DojoDachi.Controllers;
using Microsoft.AspNetCore.Http;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
         if (HttpContext.Session.GetInt32("fullness") == null)
        {
            HttpContext.Session.SetInt32("fullness",20);
        }else if(HttpContext.Session.GetInt32("fullness")<0){
            HttpContext.Session.SetInt32("fullness",0);
        }

         if (HttpContext.Session.GetInt32("happiness") == null)
        {
             HttpContext.Session.SetInt32("happiness",20);
        }else if(HttpContext.Session.GetInt32("happiness")<0){
            HttpContext.Session.SetInt32("happiness",0);
        }

          if (HttpContext.Session.GetInt32("energy") == null)
        {
             HttpContext.Session.SetInt32("energy",50); 
        }else if(HttpContext.Session.GetInt32("energy")<0){
            HttpContext.Session.SetInt32("energy",0);
        }


        if (HttpContext.Session.GetInt32("meals") == null)
        {
            HttpContext.Session.SetInt32("meals",3);
        }

        if (HttpContext.Session.GetInt32("phrase") == null)
        {
            HttpContext.Session.SetString("phrase","");
        }

        if (HttpContext.Session.GetInt32("reaction") == null)
        {
            HttpContext.Session.SetString("reaction","👍");
        }
        
        return View();
    }

    [HttpGet("feed")]
    public IActionResult feed()
    {
        int? meals=HttpContext.Session.GetInt32("meals");
        if(meals!=0){
            Random rand= new Random();
            int prob= rand.Next(0,100);
            int add=0;
            if(prob<25){
                HttpContext.Session.SetString("reaction","😑😑😑");
                
            }
            else{
                HttpContext.Session.SetString("reaction","😃😃😃");
                int? fullness=HttpContext.Session.GetInt32("fullness");
                add=rand.Next(5,10);
                HttpContext.Session.SetInt32("fullness",(int)fullness+add);
                
            }
            HttpContext.Session.SetInt32("meals",(int)meals-1);
            HttpContext.Session.SetString("phrase",$"You feeded your Dojodachi with {add}");
        
        }
        else{
            HttpContext.Session.SetString("phrase","You have no more meals");
            HttpContext.Session.SetString("reaction","");
        }
        return RedirectToAction("Index");
    }
    [HttpGet("play")]
    public IActionResult play()
    {       
            if (HttpContext.Session.GetInt32("energy")>0)
            {
                Random rand= new Random();
                int prob= rand.Next(0,100);
                int add=0;
                if(prob<25){
                    HttpContext.Session.SetString("reaction","😑😑😑");
                }
                else{
                    HttpContext.Session.SetString("reaction","😃😃😃");
                    add=rand.Next(5,10);
                    int? happiness=HttpContext.Session.GetInt32("happiness");
                    
                    HttpContext.Session.SetInt32("happiness",(int)happiness+add);
                }
                int? energy=HttpContext.Session.GetInt32("energy");
                HttpContext.Session.SetInt32("energy",(int)energy-5);
                HttpContext.Session.SetString("phrase",$"You played with your Dojodachi Happiness +{add} , Energy -5 ");
            }else{
                HttpContext.Session.SetString("phrase","You have no Energy to play");
                HttpContext.Session.SetString("reaction","");
            }
        
            
        
        return RedirectToAction("Index");
    }

    [HttpGet("work")]
    public IActionResult work()
    {       
            int? energy=HttpContext.Session.GetInt32("energy");
            if(energy>0){
                HttpContext.Session.SetInt32("energy",(int)energy-5);
                Random rand= new Random();
                int? meals=HttpContext.Session.GetInt32("meals");
                HttpContext.Session.SetInt32("meals",(int)meals+rand.Next(1,3));
            }
            else
            HttpContext.Session.SetString("phrase","You have no Energy to work");
            HttpContext.Session.SetString("reaction","");
        
        return RedirectToAction("Index");
    }

    [HttpGet("sleep")]
    public IActionResult sleep()
    {       
            int? energy=HttpContext.Session.GetInt32("energy");
            HttpContext.Session.SetInt32("energy",(int)energy+15);

            int? happiness=HttpContext.Session.GetInt32("happiness");
            HttpContext.Session.SetInt32("happiness",(int)happiness-5);

            int? fullness=HttpContext.Session.GetInt32("fullness");
            HttpContext.Session.SetInt32("fullness",(int)fullness-5);
        
        return RedirectToAction("Index");
    }
    [HttpGet("restart")]
    public IActionResult restart()
    {       
            HttpContext.Session.Clear();
        
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
