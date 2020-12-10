--In this video, we will discuss deleting all duplicate rows except one from a sql server table. 

--SQL Script to create Employees table
Create table EmployeesD
(
 ID int,
 FirstName nvarchar(50),
 LastName nvarchar(50),
 Gender nvarchar(50),
 Salary int
)
GO

Insert into EmployeesD values (1, 'Mark', 'Hastings', 'Male', 60000)
Insert into EmployeesD values (1, 'Mark', 'Hastings', 'Male', 60000)
Insert into EmployeesD values (1, 'Mark', 'Hastings', 'Male', 60000)
Insert into EmployeesD values (2, 'Mary', 'Lambeth', 'Female', 30000)
Insert into EmployeesD values (2, 'Mary', 'Lambeth', 'Female', 30000)
Insert into EmployeesD values (3, 'Ben', 'Hoskins', 'Male', 70000)
Insert into EmployeesD values (3, 'Ben', 'Hoskins', 'Male', 70000)
Insert into EmployeesD values (3, 'Ben', 'Hoskins', 'Male', 70000)

--The delete query should delete all duplicate rows except one

with result as
(
	select *, ROW_NUMBER() over (partition by Id order by id) as rownumber from EmployeesD
)
delete result where rownumber > 1

select * from EmployeesD;

select ID, ROW_NUMBER() OVER (partition by ID Order By Id desc) as RowNumberCol,
RANK() OVER(Order By Id desc) as RankCol,
DENSE_RANK() OVER(Order By Id desc) as DenseRankCol, FirstName
from EmployeesD