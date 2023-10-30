// Three Basic Arrays

int[] arr = {0,1,2,3,4,5,6,7,8,9};
string[] arr2 = {"Tim", "Martin", "Nikki", "Sara"};
bool[] arr3= new bool[10];
for (int i=0;i<9; i += 2){
arr3[i]=true;
arr3[i+1]=false;
}
for (int i=0;i<9;i++){
Console.WriteLine(arr3[i]);
}

// List of Flavors
    List<string> IceCream=new List<string> {"Strawberry","lemon","chocolate","pistachio","vanilla"};

Console.WriteLine("Length : " + IceCream.Count);
Console.WriteLine("Third one: " + IceCream[2] );
IceCream.RemoveAt(3);
Console.WriteLine("Length 2 : " + IceCream.Count);

// User Info Dictionary

Dictionary<string,string> UserInfo = new Dictionary<string, string>();
Random rand = new Random();
for(int i=0 ; i<4;i++){
    UserInfo.Add(arr2[i],IceCream[rand.Next(4)]);
}

foreach (KeyValuePair<string,string> One in UserInfo)
{
    Console.WriteLine(One.Key + " ---- " + One.Value);
}

