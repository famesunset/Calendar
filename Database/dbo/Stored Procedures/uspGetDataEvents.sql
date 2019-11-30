﻿
CREATE PROCEDURE [dbo].[uspGetDataEvents] 
	@IdUser int,
	@id_Calendar idsCalendars null readonly, 
	@dateTimeStart datetime,
	@dateTimeFinish datetime
AS
BEGIN
	
select uc.CalendarId, ca.Name as CalendarName, a.Name as AccessName, 
e.Id as EventId, e.Description, e.Title, e.RepeatId,
e.TimeStart, e.TimeFinish, e.AllDay, CalendarColor = color.Hex,
n.Before as NotificationValue, n.TimeUnitId as NotificationTimeUnitId,
uc.IsAccepted as IsCalendarAccepted

from Users u
left join UsersCalendars uc on u.Id = uc.UserId
left join Calendars ca on uc.CalendarId = ca.Id
left join Access a on a.Id = ca.AccessId
left join Events e on e.CalendarId = ca.Id
left join [Repeat] r on r.Id = e.RepeatId
left join Color color on color.Id = ca.ColorId
left join Notification n on n.EventId = e.Id

where 
u.Id = @IdUser
and @dateTimeStart <= e.TimeFinish
and @dateTimeFinish >= e.TimeStart
and ca.Id IN (select * from @id_Calendar)
and r.Id = 0

END


