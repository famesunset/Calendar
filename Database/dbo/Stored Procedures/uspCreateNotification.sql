CREATE PROCEDURE [dbo].[uspCreateNotification]
	@eventId int,
	@minutesBefore int
AS
BEGIN
	SET NOCOUNT ON;
	insert into Notification (EventId, NotificationMinute)
	values (@eventId, @minutesBefore)
	return (SELECT SCOPE_IDENTITY());
END
