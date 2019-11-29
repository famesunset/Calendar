CREATE PROCEDURE [dbo].[uspUpdateNotification]
	@eventId int,
	@before int,
	@timeUnitId int
AS
BEGIN
	DECLARE @notification int
	SET NOCOUNT ON;
	SET @notification = (SELECT TOP 1 (EventId) FROM Notification WHERE EventId = @eventId)
	PRINT @notification

	IF @notification IS NULL
		 EXEC uspCreateNotification @eventId, @before, @timeUnitId
	ELSE
		BEGIN
			UPDATE Notification
			SET Before = @before, TimeUnitId = @timeUnitId
			WHERE EventId = @eventId
		END
END
