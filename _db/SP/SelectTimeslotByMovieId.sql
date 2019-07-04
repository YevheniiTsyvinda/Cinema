IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectTimeslotByMovieId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SelectTimeslotByMovieId]
GO

CREATE PROCEDURE [dbo].[SelectTimeslotByMovieId]
@MovieId int
AS
BEGIN
	SELECT * 
	FROM Timeslots
	WHERE MovieId=@MovieId
END
