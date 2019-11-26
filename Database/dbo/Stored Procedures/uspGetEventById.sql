
CREATE PROCEDURE [dbo].[uspGetEventById] 
	@eventId int
AS
BEGIN

SELECT uc.CalendarId, ca.Name as CalendarName, a.Name as AccessName, 
e.Id as EventId, e.Description, e.Title,
e.TimeStart, e.TimeFinish, e.AllDay, color.Hex as CalendarColor

FROM Users u
left join UsersCalendars uc ON u.Id = uc.UserId
left join Calendars ca ON uc.CalendarId = ca.Id
left join Access a ON a.id = ca.AccessId
left join Events e ON e.CalendarId = ca.Id
left join Color color ON ca.ColorId = color.Id

WHERE
e.Id = @eventId

END
