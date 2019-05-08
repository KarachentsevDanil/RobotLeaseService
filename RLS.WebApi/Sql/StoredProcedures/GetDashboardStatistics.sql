CREATE OR ALTER PROCEDURE [robot].[GetDashboardStatistics]
AS
BEGIN
	SET NOCOUNT ON;

	--User Statistics
	SELECT COUNT(*) FROM [user].Users

	SELECT COUNT(*) FROM [user].Users u
	WHERE EXISTS(SELECT Id FROM [robot].[Robots] WHERE UserId = u.Id)

	SELECT COUNT(*) FROM [user].Users u
	WHERE EXISTS(SELECT Id FROM [rental].[Rentals] WHERE UserId = u.Id)

	SELECT COUNT(*) FROM [user].Users u
	WHERE EXISTS(SELECT Id FROM [robot].[Robots] WHERE UserId = u.Id) AND
		  EXISTS(SELECT Id FROM [rental].[Rentals] WHERE UserId = u.Id)

	--Robot statistics
	SELECT COUNT(*) FROM [robot].Robots r

	SELECT COUNT(*) FROM [robot].Robots r
	WHERE NOT EXISTS(SELECT Id FROM rental.Rentals WHERE RobotId = r.Id AND GETDATE() BETWEEN StartDate AND EndDate)

	--Robot Rentals
	SELECT COUNT(*) FROM [rental].Rentals r

	SELECT COUNT(*) FROM [rental].Rentals r WHERE r.[Status] = 1

	SELECT COUNT(*) FROM [rental].Rentals r WHERE r.[Status] = 0

	SELECT COUNT(*) FROM [rental].Rentals r WHERE r.[Status] = 2

	--Widget statistics
	SELECT COUNT(*) FROM [rental].Rentals r WHERE CustomerFeedback IS NOT NULL

	SELECT COUNT(*) FROM [rental].Rentals r WHERE OwnerFeedback IS NOT NULL

	SELECT COUNT(*) FROM [robot].Companies

	SELECT COUNT(*) FROM [robot].[Types]

	SELECT COUNT(*) FROM [robot].Models

END