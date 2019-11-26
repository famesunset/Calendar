CREATE PROCEDURE [dbo].[uspRemoveCalendar]
	@calendarId int
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION Transact
		BEGIN TRY

		IF @calendarId NOT IN (SELECT CalendarDefaultId FROM Users)
			BEGIN
				delete from UsersCalendars
				where UsersCalendars.CalendarId = @calendarId

				delete from Notification
				where EventId in (select Id from Events where CalendarId = @calendarId)

				--delete from EventInfinity where Eventid in (select id from Events where CalendarId = @CalendarId)
				--delete from EventSchedule where Eventid in (select id from Events where CalendarId = @calendarId)

				delete from Events where CalendarId = @calendarId

				delete from Calendars 
				where Calendars.Id = @calendarId		
				--Default calendar and its events won't be removed
			END

	  COMMIT TRANSACTION Transact
	  END TRY
	  BEGIN CATCH
		  ROLLBACK TRANSACTION Transact
	  END CATCH
END
