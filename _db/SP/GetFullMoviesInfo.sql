IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFullMoviesInfo]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[GetFullMoviesInfo]
GO

CREATE PROCEDURE [dbo].[GetFullMoviesInfo]
AS
BEGIN
	
	/*Select 1: Movies*/
	Select 
		*
	FROM
		Movies

	/*Select 2: Timeslot Tags*/

	Select
		ts.Id [TimeslotId],
		ts.StartTime [StartTime],
		tf.Cost [Cost],
		ts.MovieId
	FROM
		Timeslots ts
		JOIN Tariffs tf ON tf.Id=ts.TariffId

END
