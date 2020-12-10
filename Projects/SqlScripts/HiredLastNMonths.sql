--SQL query to find employees hired in last n months
Create table EmployeesHire
(
     ID int primary key identity,
     FirstName nvarchar(50),
     LastName nvarchar(50),
     Gender nvarchar(50),
     Salary int,
     HireDate DateTime
)
GO

Insert into EmployeesHire values('Mark','Hastings','Male',60000,'5/10/2014')
Insert into EmployeesHire values('Steve','Pound','Male',45000,'4/20/2014')
Insert into EmployeesHire values('Ben','Hoskins','Male',70000,'4/5/2014')
Insert into EmployeesHire values('Philip','Hastings','Male',45000,'3/11/2014')
Insert into EmployeesHire values('Mary','Lambeth','Female',30000,'3/10/2014')
Insert into EmployeesHire values('Valarie','Vikings','Female',35000,'2/9/2014')
Insert into EmployeesHire values('John','Stanmore','Male',80000,'2/22/2014')
Insert into EmployeesHire values('Able','Edward','Male',5000,'1/22/2014')
Insert into EmployeesHire values('Emma','Nan','Female',5000,'1/14/2014')
Insert into EmployeesHire values('Jd','Nosin','Male',6000,'1/10/2013')
Insert into EmployeesHire values('Todd','Heir','Male',7000,'2/14/2013')
Insert into EmployeesHire values('San','Hughes','Male',7000,'3/15/2013')
Insert into EmployeesHire values('Nico','Night','Male',6500,'4/19/2013')
Insert into EmployeesHire values('Martin','Jany','Male',5500,'5/23/2013')
Insert into EmployeesHire values('Mathew','Mann','Male',4500,'6/23/2013')
Insert into EmployeesHire values('Baker','Barn','Male',3500,'7/23/2013')
Insert into EmployeesHire values('Mosin','Barn','Male',8500,'8/21/2013')
Insert into EmployeesHire values('Rachel','Aril','Female',6500,'9/14/2013')
Insert into EmployeesHire values('Pameela','Son','Female',4500,'10/14/2013')
Insert into EmployeesHire values('Thomas','Cook','Male',3500,'11/14/2013')
Insert into EmployeesHire values('Malik','Md','Male',6500,'12/14/2013')
Insert into EmployeesHire values('Josh','Anderson','Male',4900,'5/1/2014')
Insert into EmployeesHire values('Geek','Ging','Male',2600,'4/1/2014')
Insert into EmployeesHire values('Sony','Sony','Male',2900,'4/30/2014')
Insert into EmployeesHire values('Aziz','Sk','Male',3800,'3/1/2014')
Insert into EmployeesHire values('Amit','Naru','Male',3100,'3/31/2014')

with result as
(
	select *, DATEDIFF(MONTH, HireDate, GETDATE()) as diff from EmployeesHire
)
select * from result where diff = 79;