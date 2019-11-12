CREATE PROCEDURE uspGetInfinityEvents
	@userId int,
	@id_Calendar idsCalendars null readonly
AS
BEGIN
	SET NOCOUNT ON;
	select uc.CalendarId as IdCalendar, ca.Name as CalendarName, a.Name as AccessName, 
	e.Id, e.Description, e.Notification, e.Title,
	ei.RepeatId, ei.TimeStart as TimePeriodStart, 
	e.TimeStart, e.TimeFinish, e.AllDay,
	ni.NotificationMinute

from Users u
left join UsersCalendars uc on u.Id = uc.UserId
left join Calendars ca on uc.CalendarId = ca.Id
left join Access a on a.id = ca.AccessId
left join Events e on e.CalendarId = ca.Id
left join EventInfinity ei on ei.EventId = e.Id
left join NotificationInfinity ni on ni.EventId = e.id

where 
u.Id = @userId
and ca.Id in (select * from @id_Calendar)
END
