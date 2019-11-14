CREATE PROCEDURE [dbo].[uspUpdateSchedule]
	@eventId int, 
	@Schedule Schedule readonly
AS
BEGIN
	BEGIN TRANSACTION Transact
  BEGIN TRY
	SET NOCOUNT ON;
	delete from EventSchedule
	where EventId = @eventId
	insert into EventSchedule
	(EventId, TimeStart, TimeFInish)
	select @eventId, * from @Schedule
 COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END

