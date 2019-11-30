CREATE PROCEDURE uspFragmentation
as
begin 

select a.object_id, 
object_name(a.object_id) AS TableName,
a.index_id, name AS IndedxName, 
avg_fragmentation_in_percent
from sys.dm_db_index_physical_stats (DB_ID (N'Calendar'), OBJECT_ID(''),NULL,NULL, NULL) AS a
inner join sys.indexes AS b on a.object_id = b.object_id and a.index_id = b.index_id
order by  a.avg_fragmentation_in_percent desc

end 
