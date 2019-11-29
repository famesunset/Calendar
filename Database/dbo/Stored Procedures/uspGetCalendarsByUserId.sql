
CREATE PROCEDURE [dbo].[uspGetCalendarsByUserId]
	@idUser int
AS
BEGIN
	select 
	c.Id, c.Name, c.AccessId, color.Id as ColorId, color.Hex as ColorHex, c.UserOwnerId as UserOwnerId from UsersCalendars uc
	left join Calendars c on uc.CalendarId = c.Id
	left join Color color on c.ColorId = color.Id
	where UserId = @idUser
END
