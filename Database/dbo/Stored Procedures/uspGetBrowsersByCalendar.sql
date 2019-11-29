
CREATE PROCEDURE [dbo].[uspGetBrowsersByCalendar]
	@calendarId int
AS
BEGIN
	SET NOCOUNT ON;
    SELECT b.Id, b.Browser AS BrowserId, b.UserId
	FROM UsersCalendars uc
	LEFT JOIN Browser b on b.UserId = uc.UserId
	WHERE uc.CalendarId = @calendarId
	
END