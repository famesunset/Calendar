--exec [uspCreateScheduledEvent] 2, 'Notification', 'Description', 'Title', '2019-10-28 10:13:34.830', '2019-10-28 10:33:34.830'
--select * from Events
--select * from EventSchedule
CREATE PROCEDURE [dbo].[uspUpdateEvent]
	@Id int,
	@CalendarId int, 
	@Description nvarchar(max) null,
	@Title nvarchar(max),
	@RepeatId int,
	@TimeStart datetime null,
	@TimeFinish datetime null, 
	@AllDay bit null
AS
BEGIN
	BEGIN TRANSACTION Transact
  BEGIN TRY
	update Events 
	set 
	CalendarId = @CalendarId, 
	Description = @Description, 
	Title = @Title, 
	TimeStart = @TimeStart, 
	TimeFinish = @TimeFinish,
	AllDay = @AllDay,
	RepeatId = @RepeatId
	where Id = @Id
  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
