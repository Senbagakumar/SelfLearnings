class Greeting
{
    greet() : void
    {        
        console.log("Hello World");
        console.log("This is my first typescript");
        var name:string = "john";
        var score1: number = 50;
        var score2: number = 45;
        var sum = score1 + score2;

        console.log("name :" +name);
        console.log("firstNumber=" + score1); 
        console.log("secondNumber="+ score2);
        console.log("sum value="+ sum);

        let num: any = "1";
        let num1:number = parseInt(num);
        let num2:number = Number(num);

        var result = num1 + 20;

        console.log("num1 type=" + typeof(num1));
        console.log("num2 casting type="+ typeof(num2));
        console.log(result); // expect to print 21 but output is 120

        var str= "1";
        var str1 = str;
        console.log(typeof(str1));

        var tnumber: number = -2;
        console.log(tnumber > 0 ? "positive" : "negative");
        console.log(typeof(tnumber));

    }
}

var obj=new Greeting();
obj.greet();