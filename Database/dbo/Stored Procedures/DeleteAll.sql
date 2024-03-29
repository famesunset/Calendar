﻿CREATE PROCEDURE [dbo].[DeleteAll]
AS
BEGIN
BEGIN TRANSACTION Transact
  BEGIN TRY
delete from UsersCalendars
delete from Users
delete from Calendars
DELETE FROM NotificationSchedule
DELETE FROM EventSchedule
DELETE FROM Events
  COMMIT TRANSACTION Transact
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION Transact
  END CATCH
  end