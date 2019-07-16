
CREATE PROCEDURE GetTimeslotTagsByMovieId
	@MovieId int
AS
BEGIN
	
	SELECT
		ts.Id [TimeslotId],
		ts.StartTime [StartTime],
		tf.Cost [Cost],
		ts.MovieId [MovieId]
	  FROM Timeslots ts
	  JOIN Tariffs tf ON tf.Id = ts.TariffId

	  Where MovieId = @MovieId
END
GO
