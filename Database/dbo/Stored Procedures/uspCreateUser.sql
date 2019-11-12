
CREATE PROCEDURE [dbo].[uspCreateUser]
	@name nvarchar(max),
	@mobile nvarchar(max),
	@email nvarchar(max),
	@identityId int
AS
BEGIN
	declare @IdCalendarDefault int
BEGIN TRANSACTION Transact
  BEGIN TRY
      insert into Calendars (Name, AccessId)
	  values ('Default', 1)

	  set @IdCalendarDefault = (select TOP 1 (Id) from Calendars
	  order by Id desc)

	  insert into Users(Name, Mobile, Email, CalendarDefaultId, IdentityId)
	  values (@name, @mobile, @email, @idCalendarDefault, @identityId)
  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
