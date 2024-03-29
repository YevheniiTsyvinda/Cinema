IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectTimeslotById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SelectTimeslotById]
GO

CREATE PROCEDURE [dbo].[SelectTimeslotById]
	@Id int
AS
BEGIN
	/* SELECT 1: Timeslots*/
	SELECT 
		* 
	FROM 
		Timeslots
	WHERE 
		Id = @Id
	
	/* SELECT 2: Requested Seats*/
	SELECT
		TimeslotId,
		Seat,
		[Row],
		[Status]
	FROM 
		RequestedSeats
	WHERE
		TimeslotId = @Id
END
