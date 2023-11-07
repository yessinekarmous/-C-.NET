using System.Reflection.Metadata;

class SweetTooth:Ninja{

    public override bool IsFull {
        get{
          if (calorieIntake>=1500){
            return true;
        }
        else{
            return false;
        }  
        }
        }
    public override void Consume(IConsumable item)
    {
        if(IsFull){
            Console.WriteLine("The Ninja is full");
        }
        else{
            
            if (item.IsSweet){
                calorieIntake+=item.Calories+10;
            }
            else{
                calorieIntake+=item.Calories;
            }
            ConsumptionHistory.Add(item);
            Console.WriteLine(item.GetInfo());
            
                    }
    }
}



    

