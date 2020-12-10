--Transform rows into columns in sql server

Create Table Countries
(
 Country nvarchar(50),
 City nvarchar(50)
)
GO

Insert into Countries values ('USA','New York')
Insert into Countries values ('USA','Houston')
Insert into Countries values ('USA','Dallas')

Insert into Countries values ('India','Hyderabad')
Insert into Countries values ('India','Bangalore')
Insert into Countries values ('India','New Delhi')

Insert into Countries values ('UK','London')
Insert into Countries values ('UK','Birmingham')
Insert into Countries values ('UK','Manchester')

select * from Countries;

--Delete from Countries where City='Dallas'

Select Country, City1, City2, City3
from
(
	select Country, City, 'City'+ cast(ROW_NUMBER() over (partition by Country order by Country) as varchar(10)) ColumnSequence from Countries
) temp
pivot
( 
	max(City) for ColumnSequence in (City1, City2, City3)
) pirt;


With temp as
(
	select Country, City, 'City'+ cast(ROW_NUMBER() over (partition by Country order by Country) as varchar(10)) ColumnSequence from Countries
) Select Country, City1, City2, City3 from temp
pivot
( 
	max(City) for ColumnSequence in (City1, City2, City3)
) pirt;