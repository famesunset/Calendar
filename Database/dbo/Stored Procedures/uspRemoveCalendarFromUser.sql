
CREATE PROCEDURE [dbo].[uspRemoveCalendarFromUser]
	@calendarId int,
	@userId int
AS
BEGIN
	SET NOCOUNT ON;
    DELETE FROM UsersCalendars 
	WHERE UserId = @userId AND CalendarId = @calendarId;
END
