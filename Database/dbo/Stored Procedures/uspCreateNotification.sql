CREATE PROCEDURE [dbo].[uspCreateNotification]
	@eventId int,
	@before int,
	@timeUnitId int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Notification (EventId, Before, TimeUnitId)
	VALUES (@eventId, @before, @timeUnitId)

	SELECT CAST(SCOPE_IDENTITY() as int)
END
