
CREATE PROCEDURE [dbo].[uspGetAccess]
	@id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, Name from Access
	where Id = @id
END
