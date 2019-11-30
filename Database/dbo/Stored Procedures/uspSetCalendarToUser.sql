
CREATE PROCEDURE [dbo].[uspSetCalendarToUser]
	@calendarId int,
	@userId int
AS
BEGIN
	SET NOCOUNT ON;
    INSERT INTO UsersCalendars (UserId, CalendarId)
	VALUES (@userId, @calendarId)

	SELECT TOP 1 Id FROM Calendars ORDER BY Id DESC
END
