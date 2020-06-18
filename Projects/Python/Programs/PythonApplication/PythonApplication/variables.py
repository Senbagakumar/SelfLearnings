#https://www.w3schools.com/python/python_syntax.asp

x=10
y=20

if(y > x):
	print("Y is greater than x")

y = y-x

if(y==x):
	print("y is equal to x")
	print("y value is=" + str(y))  # int to string conversion
	print("x value is=" + str(x))

b="10"
a=5

print(a+int(b)) #string to int conversion

# over ride the existing values. no need to type conversior/it won't throws any error.
c=10
print(c)
c="hi"
print(c)

#https://www.w3schools.com/python/python_datatypes.asp
x= int(20)

x=["hi","how","are"]
x.append("you");
x.append(10);
print(x);

#Multiline strings

abc = """hi
how
are
you """

print(abc)

sin = "sindhu"
son = "sindhu"

aa = 10
bb = 5
if(sin == son and aa == bb):
	print("both are same")
else:
	print("both are not same")

if(sin == son or aa == bb):
	print("any one is same")

