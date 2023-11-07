class Buffet
{
    public List<Food> Menu;
     
    //constructor
    public Buffet()
    {
        Menu = new List<Food>()
        {
            new Food("Example", 1000, false, false),
            new Food("Burger", 500, false, false),
            new Food("Salad", 150, false, false),
            new Food("Sushi", 400, true, false),
            new Food("Ice Cream", 250, false, true),
            new Food("Spaghetti", 350, false, false),
            new Food("Fruit Salad", 120, false, true)
        };
    }
     Random rand =new Random();
    public Food Serve()
    {
        return Menu[rand.Next(Menu.Count)];

    }
}

