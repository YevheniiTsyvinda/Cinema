IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectTariffById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SelectTariffById]
GO

CREATE PROCEDURE [dbo].[SelectTariffById]
@Id int
AS
BEGIN
	SELECT * 
	FROM Tariffs
	WHERE Id=@Id
END
