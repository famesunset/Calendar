
CREATE PROCEDURE [dbo].[uspGetDataEvents] 
	@IdUser int,
	@id_Calendar idsCalendars null readonly, 
	@dateTimeStart datetime,
	@dateTimeFinish datetime
AS
BEGIN
	
select uc.CalendarId as IdCalendar, ca.Name as CalendarName, a.Name as AccessName, 
e.Id as EventId, e.Description, e.Notification, e.Title,
es.Id as EventSchedule_id, e.TimeStart, e.TimeFInish, 
ns.NotificationTime

from Users u
left join UsersCalendars uc on u.Id = uc.UserId
left join Calendars ca on uc.CalendarId = ca.Id
left join Access a on a.id = ca.AccessId
left join Events e on e.CalendarId = ca.Id
left join EventSchedule es on es.EventId = e.Id
left join NotificationSchedule ns on ns.EventScheduleid = es.Id

where 
u.Id = @IdUser
and e.TimeStart >= @dateTimeStart 
and e.TimeFinish <= @dateTimeFinish
and ca.Id IN (select * from @id_Calendar)

END
