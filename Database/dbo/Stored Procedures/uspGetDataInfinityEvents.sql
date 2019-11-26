
CREATE PROCEDURE [dbo].[uspGetDataInfinityEvents] 
	@IdUser int,
	@id_Calendar idsCalendars null readonly,
	@date datetime
AS
BEGIN
	
select uc.CalendarId, ca.Name as CalendarName, a.Name as AccessName, 
e.Id as EventId, e.Description, e.Title,
e.TimeStart, e.TimeFInish, e.AllDay, e.RepeatId

from Users u
left join UsersCalendars uc on u.Id = uc.UserId
left join Calendars ca on uc.CalendarId = ca.Id
left join Access a on a.id = ca.AccessId
left join Events e on e.CalendarId = ca.Id
left join [Repeat] r on r.Id = e.RepeatId

where 
u.Id = @IdUser
and ca.Id IN (select * from @id_Calendar)
and r.Id != 0
and e.TimeFinish <= @date

END


