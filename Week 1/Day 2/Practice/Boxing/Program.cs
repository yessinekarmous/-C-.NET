List <object> MyList=new List<object>();
MyList.Add(7);
MyList.Add(28);
MyList.Add(-1);
MyList.Add(true);
MyList.Add("chair");

foreach(object One in MyList){
    Console.WriteLine(One);
}
int s=0;
for(int i=0;i<MyList.Count;i++){
    if (MyList[i] is int)
    {
        s= s+ (int)MyList[i];
    }
}
Console.WriteLine("The Sum is: "+ s);
Console.WriteLine($"The Sum is: {s}");