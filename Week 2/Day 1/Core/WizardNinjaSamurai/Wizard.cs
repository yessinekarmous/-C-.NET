using System.Data.Common;

class Wizard :Human
{
int intel=25;
int hp=50;
public Wizard(string name, int str, int intel, int dex, int hp) : base(name,str,intel,dex,hp){
    
}
public void Attack(Wizard target){
    int reduce=3*target.intel;
    target.hp-= reduce;

}



}