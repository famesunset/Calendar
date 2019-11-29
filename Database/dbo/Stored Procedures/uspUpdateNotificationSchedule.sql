CREATE PROCEDURE [dbo].[uspUpdateNotificationSchedule]
	@eventScheduleId int,
	@notificationTime datetime
AS
BEGIN
	SET NOCOUNT ON;
	update NotificationSchedule
	set NotificationTime = @notificationTime
	where EventScheduleId = @eventScheduleId
	return (SELECT SCOPE_IDENTITY());
END
