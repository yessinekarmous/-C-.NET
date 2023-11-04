class Card{
 string name;
 string suit;
int val;
public Card(string Name,string Suit,int Value){
    name=Name;
    suit=Suit;
    val=Value;
}
public void Print()
{
    Console.WriteLine("Card Name: "+name);
    Console.WriteLine("Card value: "+val);
    Console.WriteLine("Card suit: "+suit);
}
}