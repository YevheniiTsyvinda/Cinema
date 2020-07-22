IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectHallById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SelectHallById]
GO

CREATE PROCEDURE [dbo].[SelectHallById]
@Id int
AS
BEGIN
	SELECT * 
	FROM Halls
	WHERE Id=@Id
END
