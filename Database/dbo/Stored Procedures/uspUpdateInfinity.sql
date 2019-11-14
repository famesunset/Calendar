CREATE PROCEDURE [dbo].[uspUpdateInfinity]
	@eventId int, 
	@timeStart datetime,
	@repeatId int
AS
BEGIN
	SET NOCOUNT ON;
	delete from EventInfinity
	where EventId = @eventId
	insert into EventInfinity
	(EventId, TimeStart, RepeatId)
	values 
	(@eventId, @timeStart, @repeatId)
END
