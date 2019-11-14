
CREATE PROCEDURE [dbo].[uspGetEventById] 
	@eventId int
AS
BEGIN

SELECT uc.CalendarId AS IdCalendar, ca.Name AS CalendarName, a.Name AS AccessName, 
e.Id AS EventId, e.Description, e.Notification, e.Title,
es.Id AS EventSchedule_id, e.TimeStart, e.TimeFInish, 
ns.NotificationTime, e.AllDay

FROM Users u
left join UsersCalendars uc ON u.Id = uc.UserId
left join Calendars ca ON uc.CalendarId = ca.Id
left join Access a ON a.id = ca.AccessId
left join Events e ON e.CalendarId = ca.Id
left join EventSchedule es ON es.EventId = e.Id
left join NotificationSchedule ns ON ns.EventScheduleid = es.Id

WHERE
e.Id = @eventId

END
