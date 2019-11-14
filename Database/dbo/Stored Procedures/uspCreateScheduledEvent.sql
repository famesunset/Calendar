--declare @Schedule Schedule insert into @Schedule values ('2019-10-28 10:13:34.830', '2019-10-28 10:33:34.830')
--exec [uspCreateScheduledEvent] 2, ' Notification', 'Description', 'Title', @Schedule, '2019-10-28 10:13:34.830', '2019-10-28 10:33:34.830', 1
CREATE PROCEDURE [dbo].[uspCreateScheduledEvent]
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
declare @eventId int
	BEGIN TRANSACTION Transact
  BEGIN TRY
      exec [uspCreateEvent] @CalendarId, @Notification, @Description, @Title, @TimeStart, @TimeFinish, @AllDay
	  set @eventId = (select top 1 Id from Events order by Id desc)
	  exec [uspCreateSchedule] @eventId, @Schedule
  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
