--exec uspCreateCalendar 1, 'Calendar', 1
CREATE PROCEDURE [dbo].[uspCreateCalendar]
	@IdUser int,
	@Name nvarchar(max),
	@AccessId int,
	@ColorId int
AS
BEGIN
declare @id int
	SET NOCOUNT ON;
BEGIN TRANSACTION Transact
  BEGIN TRY

	insert into Calendars (Name, AccessId, UserOwnerId, ColorId)
	values (@Name, @AccessId, @IdUser, @ColorId)

	SET @id = SCOPE_IDENTITY()
	
	exec uspSetCalendarToUser @id, @IdUser
	exec uspAcceptCalendar @IdUser, @id

	SELECT @id
COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END