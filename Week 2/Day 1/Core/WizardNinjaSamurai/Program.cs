  Wizard wizard = new Wizard("Gandalf",5,2);
        Ninja ninja = new Ninja("Naruto",2,5,30,200);
        Samurai samurai = new Samurai("Kenshin",20,4,32);
        Human target = new Human("Enemy", 10, 10, 10, 100);

        
        wizard.Attack(target);
        wizard.Heal(target);
        ninja.Attack(target);
        ninja.Steal(target);
        samurai.Attack(target);
        samurai.Meditate(target);

        Console.WriteLine($"Health of the target: {target.Health}");
        Console.WriteLine($"Health of the Wizard: {wizard.Health}");
        Console.WriteLine($"Health of the Ninja: {ninja.Health}");
        Console.WriteLine($"Health of the Samurai: {samurai.Health}");