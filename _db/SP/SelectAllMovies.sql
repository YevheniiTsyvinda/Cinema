IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectAllMovies]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SelectAllMovies]
GO
CREATE PROCEDURE [dbo].[SelectAllMovies] 
AS
BEGIN
	SELECT * FROM Movies
END
