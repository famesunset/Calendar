
CREATE PROCEDURE [dbo].[uspGetUsersByIdCalendar]
	@idCalendar int
AS
BEGIN
	select 
	u.Id, u.Name as UserName, u.IdentityId
	from Users u
	left join UsersCalendars uc on u.Id = uc.UserId
	left join Calendars c on uc.CalendarId = c.Id
	where c.Id = @idCalendar
END
