interface IPerson
{
    firstName:string,
    lastName:string,
    printName: () => string
}

var Person:IPerson = 
{
   firstName : "senba",
   lastName : "Kumar",
   printName:(): string => { return "Hi" }
}

console.log(Person.firstName);
console.log(Person.printName());

class employee implements IPerson
{
    firstName = "Senbaga Kumar";
    lastName = "Sigamani";

    printName() { return this.firstName + this.lastName }
}

class shape
{
    Area: number;

    constructor(a:number)
    {
        this.Area=a;
    }
}

class circle extends shape
{
    disp():void
    {
        console.log("Area of the circle" + this.Area);
    }
}

var c = new circle(20);
c.disp();

var emp = new employee();
console.log(emp.printName());