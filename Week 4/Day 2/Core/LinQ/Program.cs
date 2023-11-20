List<Eruption> eruptions = new List<Eruption>()
{
    new Eruption("La Palma", 2021, "Canary Is", 2426, "Stratovolcano"),
    new Eruption("Villarrica", 1963, "Chile", 2847, "Stratovolcano"),
    new Eruption("Chaiten", 2008, "Chile", 1122, "Caldera"),
    new Eruption("Kilauea", 2018, "Hawaiian Is", 1122, "Shield Volcano"),
    new Eruption("Hekla", 1206, "Iceland", 1490, "Stratovolcano"),
    new Eruption("Taupo", 1910, "New Zealand", 760, "Caldera"),
    new Eruption("Lengai, Ol Doinyo", 1927, "Tanzania", 2962, "Stratovolcano"),
    new Eruption("Santorini", 46,"Greece", 367, "Shield Volcano"),
    new Eruption("Katla", 950, "Iceland", 1490, "Subglacial Volcano"),
    new Eruption("Aira", 766, "Japan", 1117, "Stratovolcano"),
    new Eruption("Ceboruco", 930, "Mexico", 2280, "Stratovolcano"),
    new Eruption("Etna", 1329, "Italy", 3320, "Stratovolcano"),
    new Eruption("Bardarbunga", 1477, "Iceland", 2000, "Stratovolcano")
};
// Example Query - Prints all Stratovolcano eruptions
IEnumerable<Eruption> stratovolcanoEruptions = eruptions.Where(c => c.Type == "Stratovolcano");
PrintEach(stratovolcanoEruptions, "Stratovolcano eruptions.");
// Execute Assignment Tasks here!
 
// Helper method to print each item in a List or IEnumerable.This should remain at the bottom of your class!
static void PrintEach(IEnumerable<dynamic> items, string msg = "")
{
    Console.WriteLine("\n" + msg);
    foreach (var item in items)
    {
        Console.WriteLine(item.ToString());
    }
}

System.Console.WriteLine("First one--------------");

Eruption InChile= eruptions.FirstOrDefault(a=>a.Location=="Chile");
System.Console.WriteLine(InChile);

System.Console.WriteLine("Second one--------------");

Eruption InHawaiian = eruptions.FirstOrDefault(a=>a.Location=="Hawaiian Is");
if (InHawaiian != null)
{
    Console.WriteLine(InHawaiian); 
}
else
{
    Console.WriteLine("No Hawaiian Is Eruption found.");
}

System.Console.WriteLine("Third one--------------");

Eruption After1900 = eruptions.FirstOrDefault(a=>a.ElevationInMeters>1900&& a.Location=="New Zealand");
System.Console.WriteLine(After1900);

System.Console.WriteLine("Fourth one--------------");

Eruption Plus200 = eruptions.FirstOrDefault(a=>a.ElevationInMeters>2000);
System.Console.WriteLine(Plus200);

System.Console.WriteLine("Fifth one--------------");

List<Eruption> StartsWithL= eruptions.Where(a=>a.Volcano.StartsWith("L")).ToList();
int sum=0;
foreach(Eruption one in StartsWithL){
    System.Console.WriteLine(one);
    sum=sum+1;
}
System.Console.WriteLine($"the numbers of eruptions that starts with L is :{sum}");

System.Console.WriteLine("Sixth one--------------");

int HightestOne=  eruptions.Max(a=>a.ElevationInMeters);
System.Console.WriteLine(HightestOne);

System.Console.WriteLine("Seventh one--------------");


string NameOfVolcano=eruptions.FirstOrDefault(a=>a.ElevationInMeters==HightestOne).Volcano;
System.Console.WriteLine(NameOfVolcano);

System.Console.WriteLine("Eighth one--------------");

List<Eruption> Alphabetic= eruptions.OrderBy(a=>a.Volcano).ToList();
foreach(Eruption One in Alphabetic){
    System.Console.WriteLine(One);
}

System.Console.WriteLine("Ninth one--------------");

List<Eruption> AlphabeticAndBefore1000= eruptions.Where(a=>a.Year<1000).OrderBy(a=>a.Volcano).ToList();
foreach(Eruption One in AlphabeticAndBefore1000){
    System.Console.WriteLine(One);
}

System.Console.WriteLine("Tenth one--------------");

List<string> VolcanoNames= eruptions.Select(a=>a.Volcano).ToList();
foreach(string One in VolcanoNames){
    System.Console.WriteLine(One);}