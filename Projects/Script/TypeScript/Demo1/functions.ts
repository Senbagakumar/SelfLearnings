
class listoffunctions
{
    // optional parameter.
    disp_details(id:number, name:string, mail_id?:string):void {
        console.log("ID:" + id);
        console.log("Name:" + name);
        if(mail_id != undefined)
           console.log(mail_id);
    }

    //Rest Parameters
    addNumbers(...nums:number[])
    {
        let i;
        let sum:number=0;
        for(i=0; i<nums.length; i++)
        {
            sum+= nums[i];
        }
        console.log("sum of the numbers="+ sum);
    }

    //Default Parameters
    calc_discount(price:number, rate:number = 0.50)
    {
        console.log(price*rate);
    }

    //Anonymous functions.
    logmsg = function() 
    { 
        return "hellow world";
    }

    //Anonymous functions with parameters
    res = function(a:number, b:number)
    {
        return a*b;
    }

    //Recursion
    factorial(n:number)
    {
        if(n <=1) 
            return 1;
        else
           return n * this.factorial(n-1);
           
    }
    
    //lamda functions
    foo = (x: number) => x * 5;

    //lamda statement
    foo1 = (x: number) => 
    {
        x= x * 50;
        console.log(x);
    }

    //functional overloads
    disp(name: string) : void;
    disp(id: number, name:string): void;
    

    disp(x:any, y?:any)
    {
        console.log("first parameter=" + x);
        console.log("second parameter=" + y);
    }
    
    

}

var lf = new listoffunctions();
lf.disp_details(1,"senba");
lf.disp_details(2, "sindhu", "st.sindhu@gmail.com");

lf.addNumbers(1,2,3);
lf.addNumbers(1,2,3,4,5);

lf.calc_discount(1000);
lf.calc_discount(1000,0.30);

console.log(lf.logmsg());
console.log(lf.res(10,20));

//function constructor
var myfunction = new Function("a", "b", "return a*b");
var x = myfunction(4,3);
console.log(x);

console.log(lf.factorial(6));

console.log(lf.foo(100));
console.log(lf.foo1(10));

lf.disp("senba");
lf.disp(10, "sindhu");