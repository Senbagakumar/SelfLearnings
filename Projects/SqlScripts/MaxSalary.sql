--How to find nth highest salary in SQL Server using a Sub-Query
--How to find nth highest salary in SQL Server using a CTE
--How to find the 2nd, 3rd or 15th highest salary

--Let's use the following Employees table for this demo

--Use the following script to create Employees table
Create table Employees
(
 ID int primary key identity,
 FirstName nvarchar(50),
 LastName nvarchar(50),
 Gender nvarchar(50),
 Salary int
)
GO

Insert into Employees values ('Ben', 'Hoskins', 'Male', 70000)
Insert into Employees values ('Mark', 'Hastings', 'Male', 60000)
Insert into Employees values ('Steve', 'Pound', 'Male', 45000)
Insert into Employees values ('Ben', 'Hoskins', 'Male', 70000)
Insert into Employees values ('Philip', 'Hastings', 'Male', 45000)
Insert into Employees values ('Mary', 'Lambeth', 'Female', 30000)
Insert into Employees values ('Valarie', 'Vikings', 'Female', 35000)
Insert into Employees values ('John', 'Stanmore', 'Male', 80000)

select * from Employees;

--first max
select Max(Salary) from Employees

--second max
select Max(Salary) from Employees where Salary < (select Max(Salary) from Employees)

--To find nth highest salary using Sub-Query
select top 1 Salary from
(select distinct top 3 Salary from Employees order by Salary desc) result
order by Salary

--Dense Rank will work for duplicate Salary as well.
with Result as
(
	select Salary, DENSE_RANK() Over (order by Salary desc) as DenseRank from Employees
)
select top 1 Salary from Result where DenseRank = 3

--RowNumber will work only for unique records
with Result1 as
(
	select Salary, ROW_NUMBER() Over (order by Salary desc) as RowNo from Employees
)
select top 1 Salary from Result1 where RowNo=3