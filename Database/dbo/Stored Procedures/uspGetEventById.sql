
CREATE PROCEDURE [dbo].[uspGetEventById] 
	@eventId int
AS
BEGIN

SELECT uc.CalendarId, ca.Name as CalendarName, a.Name as AccessName, 
e.Id as EventId, e.Description, e.Title, CalendarColor = color.Hex,
e.TimeStart, e.TimeFinish, e.AllDay, e.RepeatId,
n.Before as NotificationValue, n.TimeUnitId as NotificationTimeUnitId,
uc.IsAccepted as IsCalendarAccepted

FROM Users u
left join UsersCalendars uc ON u.Id = uc.UserId
left join Calendars ca ON uc.CalendarId = ca.Id
left join Access a ON a.Id = ca.AccessId
left join Events e ON e.CalendarId = ca.Id
left join Color color on color.Id = ca.ColorId
left join Notification n on n.EventId = e.Id


WHERE
e.Id = @eventId

END
