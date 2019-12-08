
CREATE PROCEDURE [dbo].[uspRemoveBrowser]
	@browser nvarchar(MAX)
AS
BEGIN
    DELETE FROM Browser WHERE Browser = @browser
END
