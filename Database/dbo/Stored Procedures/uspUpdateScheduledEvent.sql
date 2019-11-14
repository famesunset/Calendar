CREATE PROCEDURE [dbo].[uspUpdateScheduledEvent]
	@EventId int,
	@CalendarId int, 
	@Notification nvarchar(max) null,
	@Description nvarchar(max) null,
	@Title nvarchar(max),
	@Schedule Schedule readonly,
	@TimeStart datetime null,
	@TimeFinish datetime null, 
	@AllDay bit null
AS
BEGIN
	BEGIN TRANSACTION Transact
  BEGIN TRY
	update Events 
	set 
	CalendarId = @calendarId, 
	Description = @description, 
	Notification = @notification, 
	Title = @title, 
	TimeStart = @timeStart, 
	TimeFinish = @timeFinish,
	AllDay = @allDay
	where Id = @EventId
	exec [uspUpdateSchedule] @EventId, @Schedule
  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
