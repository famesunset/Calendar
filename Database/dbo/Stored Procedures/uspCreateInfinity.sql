CREATE PROCEDURE [dbo].[uspCreateInfinity]
	@eventId int, 
	@timeStart datetime,
	@repeatId int
AS
BEGIN
	SET NOCOUNT ON;
	insert into EventInfinity
	(EventId, TimeStart, RepeatId)
	values 
	(@eventId, @timeStart, @repeatId)

END
