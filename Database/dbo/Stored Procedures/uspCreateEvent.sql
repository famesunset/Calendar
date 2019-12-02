CREATE PROCEDURE [dbo].[uspCreateEvent]
	@CalendarId int, 
	@Description nvarchar(max),
	@Title nvarchar(max),
	@TimeStart datetime,
	@TimeFinish datetime, 
	@AllDay bit,
	@RepeatId int,
	@CreatorId int
AS
BEGIN
	BEGIN TRANSACTION Transact
  BEGIN TRY
	insert into Events (CalendarId, Description, Title, TimeStart, TimeFinish, AllDay, RepeatId, CreatorId)
	values (@CalendarId, @Description, @Title, @TimeStart, @TimeFinish,@AllDay, @RepeatId, @CreatorId)
	select top 1 Id from Events order by Id desc
 COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END
