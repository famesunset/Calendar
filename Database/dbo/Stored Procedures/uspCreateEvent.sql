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
	SET NOCOUNT ON;
	insert into Events (CalendarId, Description, Notification, Title, TimeStart, TimeFinish, AllDay)
	values (@calendarId, @description, @notification, @title, @timeStart, @timeFinish,@allDay)
	return (SELECT SCOPE_IDENTITY());
END
