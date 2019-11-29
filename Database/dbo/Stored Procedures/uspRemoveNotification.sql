CREATE PROCEDURE [dbo].[uspRemoveNotification]
	@eventId int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM Notification WHERE EventId = @eventId
END
