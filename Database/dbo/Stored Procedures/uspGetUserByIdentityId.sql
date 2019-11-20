
CREATE PROCEDURE [dbo].[uspGetUserByIdentityId]
	@identityId nvarchar(max)
AS
BEGIN
	select 
	u.Id as IdUser, u.Name, u.IdentityId as IdIdentity, u.Picture, 
	u.CalendarDefaultId as IdCalendarDefault, u.Email, u.Mobile
	from Users u
	left join AspNetUsers aspUser on u.IdentityId = aspUser.Id
	where aspUser.Id = @identityId
END
