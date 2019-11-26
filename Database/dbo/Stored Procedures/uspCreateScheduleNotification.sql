CREATE PROCEDURE [dbo].[uspCreateScheduleNotification]
	@eventScheduleId int,
	@notificationTime DateTime
AS
BEGIN
	SET NOCOUNT ON;
	insert into NotificationSchedule (EventScheduleId, NotificationTime)
	values (@eventScheduleId, @notificationTime)
	return (SELECT SCOPE_IDENTITY());
END
