CREATE PROCEDURE [dbo].[uspRemoveCalendar]
	@CalendarId int
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION Transact
		BEGIN TRY

	----Default calendar and its events won't be removed
		delete from UsersCalendars
		where UsersCalendars.CalendarId in (select @CalendarId)

		delete from Calendars 
		where Calendars.id = @CalendarId 
		and @CalendarId not in (select distinct CalendarDefaultId from Users)

		delete from NotificationSchedule 
		where EventScheduleId in 
		(select id from EventSchedule 
		where EventId in (
		select Id from Events where CalendarId = @CalendarId))

		delete from NotificationInfinity
		where EventId in (select Id from Events where CalendarId = @CalendarId)

		delete from EventInfinity where Eventid in (select id from Events where CalendarId = @CalendarId)
		delete from EventSchedule where Eventid in (select id from Events where CalendarId = @CalendarId)

		delete from Events where CalendarId = @CalendarId
	  COMMIT TRANSACTION Transact
	  END TRY
	  BEGIN CATCH
		  ROLLBACK TRANSACTION Transact
	  END CATCH
END
