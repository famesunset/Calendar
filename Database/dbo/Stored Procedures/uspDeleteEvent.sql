
CREATE PROCEDURE [dbo].[uspDeleteEvent] 
	@eventId int
AS
BEGIN
	declare @IdCalendarDefault int
	declare @UserId int
BEGIN TRANSACTION Transact
  BEGIN TRY
	DELETE FROM Notification WHERE EventId = @eventId
	DELETE FROM Events WHERE Id = @eventId;
  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
