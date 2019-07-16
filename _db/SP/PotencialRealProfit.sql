
CREATE PROCEDURE [dbo].[PotencialRealProfit]
	@DateFrom DateTime,
	@DateTo DateTime
AS
BEGIN
	SELECT mv.Name,
	SUM(CASE
	WHEN rs.Status = 0 THEN 0
	WHEN rs.Status = 1 THEN tf.Cost
	End)[GuaranteedProfit],
	SUM(CASE
	WHEN rs.Status = 0 THEN tf.Cost
	WHEN rs.Status = 1 THEN 0
	End)[PotencialProfit]

	 FROM Movies mv
	 JOIN Timeslots ts ON ts.MovieId = mv.Id
	 LEFT JOIN Tariffs tf ON tf.Id = ts.Id
	 LEFT JOIN RequestedSeats rs ON rs.TimeslotId = ts.Id

	 Where
		ts.StartTime>@DateFrom AND
		ts.StartTime<@DateTo
	Group By mv.Name
END
GO
