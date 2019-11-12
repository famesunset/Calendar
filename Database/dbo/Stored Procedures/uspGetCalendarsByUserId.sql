
CREATE PROCEDURE [dbo].[uspGetCalendarsByUserId]
	@idUser int
AS
BEGIN
	select 
	c.Id, c.Name, c.AccessId from UsersCalendars uc
	left join Calendars c on uc.CalendarId = c.Id
	where UserId = @idUser
END
