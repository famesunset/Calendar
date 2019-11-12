
CREATE PROCEDURE [dbo].[uspDeleteEvent] 
	@eventId int
AS
BEGIN

DELETE FROM NotificationSchedule WHERE EventScheduleId IN (SELECT Id FROM EventSchedule WHERE EventId = @eventId);
DELETE FROM EventSchedule WHERE EventId = @eventId;
DELETE FROM EventInfinity WHERE EventId = @eventId;
DELETE FROM Events WHERE Id = @eventId;

END
