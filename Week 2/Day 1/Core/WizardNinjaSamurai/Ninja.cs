
using System.Runtime;

class Ninja:Human
{
    public Ninja(string name, int str,int intel,int dex ,int hp) : base(name,str,intel,dex,hp){
    Name=name;
    Strength=str;
    Intelligence=intel;
    Dexterity=dex;
    Health=hp;
}
    public override int Attack(Human target){
        Random random= new Random();
        int x=random.Next(1,101);
        if (x<=20){
            target.Health-=Dexterity+10;
        }
        else{
            target.Health-=Dexterity;
        }
        return target.Health;
    }
    public void Steal(Human target ){
        target.Health-=5;
        Health+=5;
    }
}