
CREATE PROCEDURE [dbo].[uspGetUsersByCalendarId]
	@calendarId int
AS
BEGIN
	select 
	u.Id, u.Name as UserName, u.IdentityId
	from UsersCalendars uc
	left join Users u on u.Id = uc.CalendarId
	where uc.CalendarId = @calendarId
END
