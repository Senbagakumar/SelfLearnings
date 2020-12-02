var Greeting = /** @class */ (function () {
    function Greeting() {
    }
    Greeting.prototype.greet = function () {
        console.log("Hello World");
        console.log("This is my first typescript");
        var name = "john";
        var score1 = 50;
        var score2 = 45;
        var sum = score1 + score2;
        console.log("name :" + name);
        console.log("firstNumber=" + score1);
        console.log("secondNumber=" + score2);
        console.log("sum value=" + sum);
        var num = "1";
        var num1 = parseInt(num);
        var num2 = Number(num);
        var result = num1 + 20;
        console.log("num1 type=" + typeof (num1));
        console.log("num2 casting type=" + typeof (num2));
        console.log(result); // expect to print 21 but output is 120
        var str = "1";
        var str1 = str;
        console.log(typeof (str1));
        var tnumber = -2;
        console.log(tnumber > 0 ? "positive" : "negative");
        console.log(typeof (tnumber));
    };
    return Greeting;
}());
var obj = new Greeting();
obj.greet();
