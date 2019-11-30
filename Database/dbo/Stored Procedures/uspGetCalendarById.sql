
CREATE PROCEDURE [dbo].[uspGetCalendarById]
	@calendarId int
AS
BEGIN
	select 
	c.Id, c.Name, c.AccessId, color.Id as ColorId, color.Hex as ColorHex, 
	c.UserOwnerId as UserOwnerId, uc.IsAccepted
	from UsersCalendars uc
	left join Calendars c on uc.CalendarId = c.Id
	left join Color color on c.ColorId = color.Id
	where c.Id = @calendarId
END
