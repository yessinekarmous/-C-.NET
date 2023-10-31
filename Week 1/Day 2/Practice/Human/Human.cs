class Human
{
    // Properties for Human
    public string Name;
    public int Strength;
    public int Intelligence;
    public int Dexterity;
    public int Health;
    // Add a constructor that takes a value to set Name, and set the remaining fields to default values
     public Human(string name,int strength=3, int intelligence=3,int dexterity=3,int health=100)
     {
        Name=name;
        Strength=strength;
        Intelligence=intelligence;
        Dexterity=dexterity;
        Health=health;
     }
    // Build Attack method
    public int Attack(Human target)
    {
        int damage=target.Strength*3;
        target.Health=target.Health-damage;
        return target.Health;
    }
}

