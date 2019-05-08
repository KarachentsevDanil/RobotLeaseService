CREATE OR ALTER PROCEDURE [robot].[GetMostValuableRobots]
	@userId nvarchar(50) = '',
	@typeId int = 0,
	@take int = 5
AS
BEGIN
	SET NOCOUNT ON;

	 SELECT [Id]
		   ,[DailyCosts]
		   ,[Name]
		   ,[CompanyName]
		   ,[RobotTypeName]
		   ,[Icon]
		   ,[Photo]
		   ,(RobotRatingSum.RatingSum / RobotRatingCount.RatingCount) as AverageRating
	  FROM [robot].[RobotView] as Robot
	  OUTER APPLY(SELECT SUM(RobotRating) as RatingSum FROM rental.Rentals WHERE RobotId = Robot.Id AND RobotRating > 0) As RobotRatingSum
	  OUTER APPLY(SELECT COUNT(*) as RatingCount FROM rental.Rentals WHERE RobotId = Robot.Id AND RobotRating > 0) As RobotRatingCount
	  WHERE UserId != @userId AND (RobotRatingSum.RatingSum / RobotRatingCount.RatingCount) > 0 AND TypeId = @typeId
	  ORDER BY AverageRating desc;
END