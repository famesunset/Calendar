
CREATE PROCEDURE [dbo].[uspGetUserById]
	@id int
AS
BEGIN
	select 
	u.Id as IdUser, u.Name, u.IdentityId as IdIdentity, u.Picture, 
	u.CalendarDefaultId as IdCalendarDefault, u.Email, u.Mobile
	from Users u
	where u.Id = @id
END
