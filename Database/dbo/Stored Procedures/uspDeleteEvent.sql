
CREATE PROCEDURE [dbo].[uspDeleteEvent] 
	@eventId int
AS
BEGIN

DELETE FROM Events WHERE Id = @eventId;

END
