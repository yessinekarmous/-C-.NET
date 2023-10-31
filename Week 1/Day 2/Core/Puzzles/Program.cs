// Random Array
int[] Array=new int[10] ;
Random rand=new Random();
for(int i=0;i<10;i++){
    Array[i]=rand.Next(5,25);
}
int max=Array[0];
int min=Array[0];
Console.WriteLine("The Array: ");
for(int i=0 ;i<10;i++){
    Console.WriteLine(Array[i]);
}
for(int i=1;i<9;i++){
    if(Array[i]>max){
        max=Array[i];
    }
    if(Array[i]<min){
        min=Array[i];
    }
}

Console.WriteLine("Max is : "+max);
Console.WriteLine("Min is : "+min);
int s=0;
for(int i=0 ;i<10;i++){
    s= s+Array[i];
}
Console.WriteLine("The Sum = "+s);

// Coin Flip
void TossCoin(){
    Console.WriteLine("Tossing a Coin!");
string[] Toss={"Heads","Tails"};
string result= Toss[rand.Next(2)];
Console.WriteLine(result);
}
TossCoin();



// Names

List<string> Names(){
    List<string> Names= new List<string> {"Todd", "Tiffany", "Charlie", "Geneva", "Sydney"};
    List<string> Second= new List<string>() ;
    for (int i=0;i<Names.Count;i++){
        if(Names[i].Length>5){
            Second.Add(Names[i]);
        }
    }
    return Second;
}

List<string> MyList=Names();
Console.Write("The List: ");
foreach (string name in MyList)
{
    Console.Write(name + " ");
}
