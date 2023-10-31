static void PrintNumbers()
{
    for(int i=1;i<255;i++){
        Console.WriteLine(i);
    }
}

static void PrintOdds()
{
    for(int i=1;i<255;i++){
        if(i%2!=0)
        { Console.WriteLine(i);}
       
    }
}


static void PrintSum()
{int s=0;
      for(int i=1;i<255;i++){
       Console.WriteLine("New Number: "+i+"sum: "+s);
        s=s+i;
    }
}

static void LoopArray(int[] numbers)
{
    // Write a function that would iterate through each item of the given integer array and 
    // print each value to the console. 
}

