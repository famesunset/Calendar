CREATE PROCEDURE [dbo].[uspGetInfinityEvents]
	@userId int,
	@id_Calendar idsCalendars null readonly
AS
BEGIN
	SET NOCOUNT ON;
	select uc.CalendarId as IdCalendar, ca.Name as CalendarName, a.Name as AccessName, 
	e.Id, e.Description, e.Title,
	e.TimeStart, e.TimeFinish, e.AllDay,
	ni.NotificationMinute

from Users u
left join UsersCalendars uc on u.Id = uc.UserId
left join Calendars ca on uc.CalendarId = ca.Id
left join Access a on a.Id = ca.AccessId
left join Events e on e.CalendarId = ca.Id
left join NotificationInfinity ni on ni.EventId = e.Id

where 
u.Id = @userId
and ca.Id in (select * from @id_Calendar)
END
