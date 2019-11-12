--exec uspCreateCalendar 1, 'Calendar', 1
CREATE PROCEDURE [dbo].[uspCreateCalendar]
	@IdUser int,
	@Name nvarchar(max),
	@AccessId int
AS
BEGIN
declare @CalendarsUsers calendarsUsers
	SET NOCOUNT ON;
BEGIN TRANSACTION Transact
  BEGIN TRY

	insert into Calendars (Name, AccessId)
	values (@Name, @AccessId)

	insert into @CalendarsUsers (id_User, id_Calendar)
	values(
	@IdUser,
	(select top 1 Id from Calendars order by Id desc))

	exec uspSetCalendarsToUser @CalendarsUsers

COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH  
END