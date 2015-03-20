CREATE TABLE #Tasks (
	TaskId INT IDENTITY
	, Name nvarchar(100)
	, [Description] nvarchar(1000)
	, Completed bit		
)

insert into #Tasks (Name, [Description], Completed)
(
select 'Pickup Groceries', 'Get Eggs, Milk, Bread, and Yogurt', 0
union all
select 'Call Mom!', 'She Misses you. Phone Number: 555-555-5555', 0
union all
select 'Get Morning Coffee', 'Extra Bold', 1
)

select * from #Tasks
drop table #Tasks

