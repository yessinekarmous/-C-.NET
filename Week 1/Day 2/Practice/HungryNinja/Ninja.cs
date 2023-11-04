class Ninja
{
    private int calorieIntake;
    public List<Food> FoodHistory;
     
    public Ninja()
     {
        calorieIntake=0;
        FoodHistory=new List<Food>();
     }
    bool IsFull
    {
        get{
            return calorieIntake>1200;  
        }
    }
     
    // build out the Eat method
    public void Eat(Food item)
    {
        if (!IsFull){
            calorieIntake+=item.Calories;
            FoodHistory.Add(item);
            Console.WriteLine("the Name of the item: "+item+"and it is"+item.IsSpicy);
        }
        else{
            Console.WriteLine("The Ninja is Full");
        }
    }
}

