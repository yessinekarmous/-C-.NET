for(int i=1;i<255; i++)
{
    Console.WriteLine(i);
}

for(int i=1;i<100;i++)
{
 if (i % 3 == 0 || i % 5 == 0){
    Console.WriteLine(i);
 }

}
for(int i=1;i<100;i++)
{
 if (i % 3 == 0 ){
    Console.WriteLine("FIZZ");
 }
 else if(i%5==0){
    Console.WriteLine("BUZZ");
 }
 else if(i % 3 == 0 || i % 5 == 0){
    Console.WriteLine("FIZZBUZZ");
 }
}
// with While 
int j =1;
while (j<100){
     if (j % 3 == 0 ){
    Console.WriteLine("FIZZ");
 }
 else if(j%5==0){
    Console.WriteLine("BUZZ");
 }
 else if(j % 3 == 0 || j % 5 == 0){
    Console.WriteLine("FIZZBUZZ");
 }
 j++;
}