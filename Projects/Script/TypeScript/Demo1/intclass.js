var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Person = {
    firstName: "senba",
    lastName: "Kumar",
    printName: function () { return "Hi"; }
};
console.log(Person.firstName);
console.log(Person.printName());
var employee = /** @class */ (function () {
    function employee() {
        this.firstName = "Senbaga Kumar";
        this.lastName = "Sigamani";
    }
    employee.prototype.printName = function () { return this.firstName + this.lastName; };
    return employee;
}());
var shape = /** @class */ (function () {
    function shape(a) {
        this.Area = a;
    }
    return shape;
}());
var circle = /** @class */ (function (_super) {
    __extends(circle, _super);
    function circle() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    circle.prototype.disp = function () {
        console.log("Area of the circle" + this.Area);
    };
    return circle;
}(shape));
var c = new circle(20);
c.disp();
var emp = new employee();
console.log(emp.printName());
