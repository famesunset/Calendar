
CREATE PROCEDURE [dbo].[uspAcceptCalendar]
	@userId int,
	@calendarId int
AS
BEGIN
	UPDATE UsersCalendars 
	SET IsAccepted = 1
	WHERE UserId = @userId AND CalendarId = @calendarId
END
