IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddRequestedSeatsToTimeslot]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[AddRequestedSeatsToTimeslot]
GO

CREATE PROCEDURE [dbo].[AddRequestedSeatsToTimeslot]
	@TimeslotId int,
	@seatsRequest As [dbo].[TimeslotSeatRequest] Readonly
AS
BEGIN
	
	INSERT INTO 
		RequestedSeats
		([Row],
		[Seat],
		[Status],
		[TimeslotId])
	Select 
		[Row],
		[Seat],
		[Status],
		@TimeslotId
	FROM
		@seatsRequest

END
