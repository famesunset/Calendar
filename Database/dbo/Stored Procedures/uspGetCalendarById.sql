
CREATE PROCEDURE [dbo].[uspGetCalendarById]
	@calendarId int
AS
BEGIN
	select 
	c.Id, c.Name, c.AccessId from UsersCalendars uc
	left join Calendars c on uc.CalendarId = c.Id
	where c.Id = @calendarId
END
