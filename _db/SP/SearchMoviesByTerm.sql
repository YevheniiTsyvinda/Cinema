IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SearchMoviesByTerm]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SearchMoviesByTerm]
GO

CREATE PROCEDURE [dbo].[SearchMoviesByTerm]
	@Term nvarchar(100)
AS
BEGIN
	
	Declare @SqlTerm nvarchar(100) = '%'+@Term+'%'
	Declare @SelectedMovieIds TABLE(Id int)
	
	INSERT INTO 
		@SelectedMovieIds
	SELECT 
		Id 
	FROM 
		Movies
	WHERE 
		[Description] like @SqlTerm OR
		[Name] like @SqlTerm

	/*Select 1: Movies*/
	Select 
		*
	FROM
		Movies
	Where
		Id = ANY (SELECT Id from @SelectedMovieIds)

	/*Select 2: Timeslot Tags*/

	Select
		ts.Id [TimeslotId],
		ts.StartTime [StartTime],
		tf.Cost [Cost],
		ts.MovieId
	FROM
		Timeslots ts
		JOIN Tariffs tf ON tf.Id=ts.TariffId
	Where
		ts.MovieId = ANY (SELECT Id from @SelectedMovieIds)

END
