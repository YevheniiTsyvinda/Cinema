USE [CinemaDataBase]
GO
/****** Object:  StoredProcedure [dbo].[SelectAllMovies]    Script Date: 09.07.2019 17:47:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SelectMovieById]
@Id int
AS
BEGIN
	
	SELECT * 
	FROM Movies
	Where Id = @Id
END
