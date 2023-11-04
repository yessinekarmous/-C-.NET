using System.Data.Common;

class Wizard :Human
{

public Wizard(string name, int str, int dex ,int intel =25, int hp=50) : base(name,str,intel,dex,hp){
    Name=name;
    Strength=str;
    Intelligence=intel;
    Dexterity=dex;
    Health=hp;
}
public override int Attack(Human target)
{
    int reduce=3*Intelligence;
    target.Health-= reduce;
    Health+=reduce;
    return target.Health;

}
public void Heal(Human target)
{
    target.Health+= Intelligence *3;

}



}