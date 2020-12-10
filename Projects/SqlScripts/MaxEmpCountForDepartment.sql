--Write a SQL query to retrieve rows that contain only numerical data.

Create Table TestTable
(
 ID int identity primary key,
 Value nvarchar(50)
)

Insert into TestTable values ('123')
Insert into TestTable values ('ABC')
Insert into TestTable values ('DEF')
Insert into TestTable values ('901')
Insert into TestTable values ('JKL')

select * from TestTable where ISNUMERIC(Value) = 1


--SQL query that retrieves the department name with maximum number of employees

Create Table Departments
(
     DepartmentID int primary key,
     DepartmentName nvarchar(50)
)
GO

Create Table EmployeesJ
(
     EmployeeID int primary key,
     EmployeeName nvarchar(50),
     DepartmentID int foreign key references Departments(DepartmentID)
)
GO

Insert into Departments values (1, 'IT')
Insert into Departments values (2, 'HR')
Insert into Departments values (3, 'Payroll')
insert into Departments values (4, 'Accounts')
GO

Insert into EmployeesJ values (1, 'Mark', 1)
Insert into EmployeesJ values (2, 'John', 1)
Insert into EmployeesJ values (3, 'Mike', 1)
Insert into EmployeesJ values (4, 'Mary', 2)
Insert into EmployeesJ values (5, 'Stacy', 3)


select top 1 d.DepartmentName, count(*) as EmployeeCount from 
Departments d inner join EmployeesJ j
on d.DepartmentID = j.DepartmentID
group by d.DepartmentName
order by count(*) desc


--Question 1: Can you list different types of JOINS available in SQL Server
--Answer: Inner Join, Left Join, Right Join, Full Join and Cross Join

--Question 2: Can you tell me the purpose of Right Join?
--Answer: Right Join returns all rows from the Right Table irrespective of whether a match exists in the left table or not.

--Question 3: Can you give me an example?
--Answer: Consider Departments and Employees tables.

--In this case we use RIGHT JOIN To retrieve all Department and Employee names, irrespective of whether a Department has Employees or not.

select d.DepartmentName, ej.EmployeeName from EmployeesJ ej right join Departments d
on d.DepartmentID = ej.DepartmentID

select d.DepartmentName, count(ej.DepartmentID) as Empcount from EmployeesJ ej right join Departments d
on d.DepartmentID = ej.DepartmentID
group by d.DepartmentName
order by count(ej.DepartmentID) desc

--Can we join two tables without primary foreign key relation ?
--Yes, we can join two tables without primary foreign key relation as long as the column values involved in the join can be converted to one type.

--if primary foreign key relation is not mandatory for 2 tables to be joined then what is the use of these keys?
--Primary key enforces uniqueness of values over one or more columns. Since ID is not a primary key in Departments table, 2 or more departments may end up having same ID value, 
--which makes it impossible to distinguish between them based on the ID column value.

--Foreign key enforces referential integrity. Without foreign key constraint on DepartmentId column in Employees table, it is possible to insert a row into Employees table 
--with a value for DepartmentId column that does not exist in Departments table.

--Ex:

--The following insert statement, successfully inserts a new Employee into Employees table whose DepartmentId is 100. But we don't have a department with ID = 100 in Departments table. 
--This means this employee row is an orphan row, and the referential integrity is lost as result
--Insert into Employees values (8, 'Mary', 'Female', 80000, 100)

--If we have had a foreign key constraint on DepartmentId column in Employees table, the following insert statement would have failed with the following error.
--Msg 547, Level 16, State 0, Line 1
--The INSERT statement conflicted with the FOREIGN KEY constraint. The conflict occurred in database "Sample", table "dbo.Departments", column 'ID'.



--Difference between blocking and deadlocking

--SQL Script to create the tables and populate them with test data
Create table TableA
(
 Id int identity primary key,
 Name nvarchar(50)
)
Go

Insert into TableA values ('Mark')
Go

Create table TableB
(
 Id int identity primary key,
 Name nvarchar(50)
)
Go

Insert into TableB values ('Mary')
Go

--Blocking : Occurs if a transaction tries to acquire an incompatible lock on a resource that another transaction has already locked. 
--The blocked transaction remains blocked until the blocking transaction releases the lock. 

--Example : Open 2 instances of SQL Server Management studio. From the first window execute Transaction 1 code and from the second window execute Transaction 2 code.
--Notice that Transaction 2 is blocked by Transaction 1. Transaction 2 is allowed to move forward only when Transaction 1 completes.

--Transaction 1
Begin Tran
Update TableA set Name='Mark Transaction 1' where Id = 1
Waitfor Delay '00:00:10'
Commit Transaction

--Transaction 2
Begin Tran
Update TableA set Name='Mark Transaction 2' where Id = 1
Commit Transaction

--Deadlock : Occurs when two or more transactions have a resource locked, and each transaction requests a lock on the resource that another transaction has already locked. 
--Neither of the transactions here can move forward, as each one is waiting for the other to release the lock. So in this case, SQL Server intervenes and ends 
--the deadlock by cancelling one of the transactions, so the other transaction can move forward.

--Example : Open 2 instances of SQL Server Management studio. From the first window execute Transaction 1 code and from the second window execute Transaction 2 code. 
--Notice that there is a deadlock between Transaction 1 and Transaction 2.

-- Transaction 1
Begin Tran
Update TableA Set Name = 'Mark Transaction 1' where Id = 1

-- From Transaction 2 window execute the first update statement

Update TableB Set Name = 'Mary Transaction 1' where Id = 1

-- From Transaction 2 window execute the second update statement
Commit Transaction

-- Transaction 2
Begin Tran
Update TableB Set Name = 'Mark Transaction 2' where Id = 1

-- From Transaction 1 window execute the second update statement

Update TableA Set Name = 'Mary Transaction 2' where Id = 1

-- After a few seconds notice that one of the transactions complete 
-- successfully while the other transaction is made the deadlock victim
Commit Transaction

select * from TableA

--Sql query to select all names that start with a given letter without like operator

select * from EmployeesJ;

--SQL query to retrieve all student names that start with letter 'M' without using the LIKE operator.

select * from EmployeesJ where CHARINDEX('M',EmployeeName)=1;
select * from EmployeesJ where left(EmployeeName,1)='M';
select * from EmployeesJ where SUBSTRING(EmployeeName,1,1)='M';
