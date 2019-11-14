--exec [uspCreateScheduledEvent] 2, 'Notification', 'Description', 'Title', '2019-10-28 10:13:34.830', '2019-10-28 10:33:34.830'
--select * from Events
--select * from EventSchedule
CREATE PROCEDURE [dbo].[uspCreateInfinityEvent]
	@CalendarId int, 
	@Notification nvarchar(max) null,
	@Description nvarchar(max) null,
	@Title nvarchar(max),
	@RepeatId int,
	@TimeStart datetime null,
	@TimeFinish datetime null, 
	@AllDay bit null
AS
BEGIN
declare @eventId int
	BEGIN TRANSACTION Transact
  BEGIN TRY
      exec [uspCreateEvent] @CalendarId, @Notification, @Description, @Title, @TimeStart, @TimeFinish, @AllDay

	  set @eventId = (select top 1 Id from Events order by Id desc)

	  exec [uspCreateInfinity] @eventId, @TimeStart, @RepeatId

  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
