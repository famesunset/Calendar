
CREATE PROCEDURE [dbo].[uspSetCalendarsToUser]
	@CalendarsUsers CalendarsUsers readonly
AS
BEGIN
	SET NOCOUNT ON;
    insert into UsersCalendars (UserId, CalendarId)
	select * from @CalendarsUsers
END
