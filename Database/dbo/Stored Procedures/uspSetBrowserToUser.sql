
CREATE PROCEDURE [dbo].[uspSetBrowserToUser]
	@userId int,
	@browser nvarchar(MAX)
AS
BEGIN
	DECLARE @browserId int
	SET NOCOUNT ON;
    INSERT INTO Browser (Browser, UserId)
	VALUES (@browser, @userId)
END
