
create PROCEDURE uspCheckDefaultCalendar
	@idCalendar int 
AS
BEGIN
declare @bool bit
declare @tempId int
set @tempId = 
	(select distinct CalendarDefaultId from Users
	where CalendarDefaultId = @idCalendar)
if (@tempId is not null)
begin
	select 1
end
if (@tempId is null)
begin
	select 0
end

END
