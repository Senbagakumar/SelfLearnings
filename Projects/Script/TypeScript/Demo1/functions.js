var listoffunctions = /** @class */ (function () {
    function listoffunctions() {
        //Anonymous functions.
        this.logmsg = function () {
            return "hellow world";
        };
        //Anonymous functions with parameters
        this.res = function (a, b) {
            return a * b;
        };
        //lamda functions
        this.foo = function (x) { return x * 5; };
        //lamda statement
        this.foo1 = function (x) {
            x = x * 50;
            console.log(x);
        };
    }
    // optional parameter.
    listoffunctions.prototype.disp_details = function (id, name, mail_id) {
        console.log("ID:" + id);
        console.log("Name:" + name);
        if (mail_id != undefined)
            console.log(mail_id);
    };
    //Rest Parameters
    listoffunctions.prototype.addNumbers = function () {
        var nums = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            nums[_i] = arguments[_i];
        }
        var i;
        var sum = 0;
        for (i = 0; i < nums.length; i++) {
            sum += nums[i];
        }
        console.log("sum of the numbers=" + sum);
    };
    //Default Parameters
    listoffunctions.prototype.calc_discount = function (price, rate) {
        if (rate === void 0) { rate = 0.50; }
        console.log(price * rate);
    };
    //Recursion
    listoffunctions.prototype.factorial = function (n) {
        if (n <= 1)
            return 1;
        else
            return n * this.factorial(n - 1);
    };
    listoffunctions.prototype.disp = function (x, y) {
        console.log("first parameter=" + x);
        console.log("second parameter=" + y);
    };
    return listoffunctions;
}());
var lf = new listoffunctions();
lf.disp_details(1, "senba");
lf.disp_details(2, "sindhu", "st.sindhu@gmail.com");
lf.addNumbers(1, 2, 3);
lf.addNumbers(1, 2, 3, 4, 5);
lf.calc_discount(1000);
lf.calc_discount(1000, 0.30);
console.log(lf.logmsg());
console.log(lf.res(10, 20));
//function constructor
var myfunction = new Function("a", "b", "return a*b");
var x = myfunction(4, 3);
console.log(x);
console.log(lf.factorial(6));
console.log(lf.foo(100));
console.log(lf.foo1(10));
lf.disp("senba");
lf.disp(10, "sindhu");
