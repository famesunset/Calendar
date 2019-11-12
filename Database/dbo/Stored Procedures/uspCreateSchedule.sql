CREATE PROCEDURE [dbo].[uspCreateSchedule]
	@eventId int, 
	@Schedule Schedule readonly
AS
BEGIN
	SET NOCOUNT ON;

	--SELECT @eventId, * FROM @Schedule

	insert into EventSchedule
	(EventId, TimeStart, TimeFInish)
	select @eventId, * from @Schedule

	--return (SELECT SCOPE_IDENTITY());
END
