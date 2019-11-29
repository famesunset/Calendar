
CREATE PROCEDURE [dbo].[uspCreateUser]
	@name nvarchar(max),
	@mobile nvarchar(max),
	@email nvarchar(max),
	@identityId nvarchar(max),
	@picture nvarchar(max)
AS
BEGIN
	declare @IdCalendarDefault int
	declare @UserId int
BEGIN TRANSACTION Transact
  BEGIN TRY
	  set @IdCalendarDefault = (select TOP 1 (Id) + 1 from Calendars
	  order by Id desc)

	  insert into Users(Name, Mobile, Email, CalendarDefaultId, IdentityId, Picture)
	  values (@name, @mobile, @email, @IdCalendarDefault, @identityId, @picture)

	  set @UserId = (select TOP 1 (Id) from Users
	  order by Id desc)

  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
