CREATE PROCEDURE [dbo].[uspCreateEvent]
	@calendarId int, 
	@notification nvarchar(max),
	@description nvarchar(max),
	@title nvarchar(max),
	@timeStart datetime,
	@timeFinish datetime, 
	@allDay bit
AS
BEGIN
	BEGIN TRANSACTION Transact
  BEGIN TRY
	insert into Events (CalendarId, Description, Notification, Title, TimeStart, TimeFinish, AllDay)
	values (@calendarId, @description, @notification, @title, @timeStart, @timeFinish,@allDay)
	select top 1 Id from Events order by Id desc
 COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
