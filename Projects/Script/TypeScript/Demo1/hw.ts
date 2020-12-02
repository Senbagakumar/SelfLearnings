var n:number = 5;
while(n > 0)
{
    console.log("HW"+n);
    n--;
}

do
{
    console.log("HW" + n);
 } while(n > 0);


 n=10;

 for(let i=0; i<n; i++)
 {
     if(i % 2 == 0)
         continue;
     else
         console.log("ith value ="+ i);
 }