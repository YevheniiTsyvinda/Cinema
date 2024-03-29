
CREATE PROCEDURE UnprofitableMovies
	@DateFrom Datetime,
	@DateTo Datetime,
	@Threshold Money
AS
BEGIN
	SELECT ResultTable.[Name] [MovieName],
	ResultTable.[GuaranteedProfit] [Profit]
	From(
		SELECT
			mv.Name,
			SUM(CASE
			When rs.Status = 0 Then tf.Cost
			When rs.Status = 1 Then 0
		END) [GuaranteedProfit]
			From Movies mv
			JOIN Timeslots ts ON ts.MovieId = mv.Id
			Left Join Tariffs tf On tf.Id = ts.TariffId
			Left Join RequestedSeats rs ON rs.TimeslotId = ts.Id
		Where
			ts.StartTime>@DateFrom And
			ts.StartTime<@DateTo
		Group By mv.Name) ResultTable
	Where
		ResultTable.GuaranteedProfit < @Threshold OR
		ResultTable.GuaranteedProfit IS NULL
END
GO
