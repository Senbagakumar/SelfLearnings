--Here is the problem definition:
--1. Employees table contains the following columns 
--    a) EmployeeId, 
--    b) EmployeeName 
--    c) ManagerId 
--2. If an EmployeeId is passed, the query should list down the entire organization hierarchy i.e who is the manager of the EmployeeId passed and who is managers manager and so on till full hierarchy is listed.

--For example, 
--Scenario 1: If we pass David's EmployeeId to the query, then it should display the organization hierarchy starting from David.

--Scenario 2: If we pass Lara's EmployeeId to the query, then it should display the organization hierarchy starting from Lara.

--We will be Employees table for this demo. SQL to create and populate Employees table with test data
Create table EmployeesH
(
 EmployeeID int primary key identity,
 EmployeeName nvarchar(50),
 ManagerID int foreign key references EmployeesH(EmployeeID)
)
GO

Insert into EmployeesH values ('John', NULL)
Insert into EmployeesH values ('Mark', NULL)
Insert into EmployeesH values ('Steve', NULL)
Insert into EmployeesH values ('Tom', NULL)
Insert into EmployeesH values ('Lara', NULL)
Insert into EmployeesH values ('Simon', NULL)
Insert into EmployeesH values ('David', NULL)
Insert into EmployeesH values ('Ben', NULL)
Insert into EmployeesH values ('Stacy', NULL)
Insert into EmployeesH values ('Sam', NULL)
GO

Update EmployeesH Set ManagerID = 8 Where EmployeeName IN ('Mark', 'Steve', 'Lara')
Update EmployeesH Set ManagerID = 2 Where EmployeeName IN ('Stacy', 'Simon')
Update EmployeesH Set ManagerID = 3 Where EmployeeName IN ('Tom')
Update EmployeesH Set ManagerID = 5 Where EmployeeName IN ('John', 'Sam')
Update EmployeesH Set ManagerID = 4 Where EmployeeName IN ('David')

GO

SELECT * FROM EmployeesH

DECLARE @EID INT
SET @EID=9;

with result2 as
(
	SELECT EmployeeID, EmployeeName, ManagerID FROM EmployeesH
	where EmployeeID=@EID

	union all

	select e1.EmployeeID, e1.EmployeeName, e1.ManagerID FROM EmployeesH e1
	JOIN result2 r1 on e1.EmployeeID = r1.ManagerID

)
select e1.EmployeeName, ISNULL(e2.EmployeeName, 'NO BOSS') as ManagerName from result2 e1
left join result2 e2 on e1.ManagerID = e2.EmployeeID;



