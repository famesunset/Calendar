
CREATE PROCEDURE [dbo].[uspCreateUser]
	@name nvarchar(max),
	@mobile nvarchar(max),
	@email nvarchar(max),
	@identityId nvarchar(max),
	@picture nvarchar(max)
AS
BEGIN
	declare @CalendarId int
	declare @UserId int
BEGIN TRANSACTION Transact
  BEGIN TRY
	  insert into Users(Name, Mobile, Email, CalendarDefaultId, IdentityId, Picture)
	  values (@name, @mobile, @email, 1, @identityId, @picture)

	  set @UserId = (select TOP 1 (Id) from Users
	  order by Id desc)

	  exec uspCreateCalendar @UserId, 'Default', 1, 1

	  set @CalendarId = (select TOP 1 (Id) from Calendars order by Id desc)
	  UPDATE Users SET CalendarDefaultId = @CalendarId WHERE Id = @UserId

  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END