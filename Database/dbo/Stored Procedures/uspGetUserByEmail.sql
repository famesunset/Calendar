
CREATE PROCEDURE [dbo].[uspGetUserByEmail]
	@email nvarchar(max)
AS
BEGIN
	SELECT 
	u.Id AS IdUser, u.Name, u.Picture, u.CalendarDefaultId as IdCalendarDefault, u.Email, u.Mobile
	FROM Users u
	WHERE u.Email = @email
END
