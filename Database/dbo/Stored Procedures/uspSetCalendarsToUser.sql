
CREATE PROCEDURE [dbo].[uspSetCalendarsToUser]
	@CalendarsUsers CalendarsUsers readonly
AS
BEGIN
	SET NOCOUNT ON;
    insert into UsersCalendars (UserId, CalendarId)
	select * from @CalendarsUsers

	select top 1 Id from Calendars order by Id desc
	--return (SELECT SCOPE_IDENTITY());
END
