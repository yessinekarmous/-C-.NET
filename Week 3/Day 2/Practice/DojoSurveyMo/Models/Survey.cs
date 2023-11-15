using System.ComponentModel.DataAnnotations;
namespace DojoSurveyMo.Models;
#pragma warning disable CS8618

public class Survey
{
    public string Name{get;set;}
    public string location{get;set;}

    public string language{get;set;}
    public string comment {get;set;}
}

