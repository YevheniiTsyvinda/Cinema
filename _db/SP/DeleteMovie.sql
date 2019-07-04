If EXISTS(Select * from sys.objects Where object_id = OBJECT_ID(N'[dbo].[DeleteMovie]') AND type in (N'P', N'PC'))
Drop procedure [dbo].[DeleteMovie]
GO

CREATE PROCEDURE DeleteMovie
	@Id int
AS
BEGIN
	DELETE Movies 
	WHERE Id = @Id
END
GO
