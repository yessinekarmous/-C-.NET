class Samurai:Human{

    public Samurai(string name, int str, int intel, int dex, int hp=200):base(name,str,intel,dex,hp){
    Name=name;
    Strength=str;
    Intelligence=intel;
    Dexterity=dex;
    Health=hp;
    }
    public  override int Attack(Human target){
        target.Health=base.Attack(target);
        if (target.Health<50){
            target.Health=0;
        }
        return target.Health;
    }
    public void Meditate (Human target){
        target.Health=200;
    }
}