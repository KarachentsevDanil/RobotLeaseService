CREATE OR ALTER PROCEDURE [robot].[GetRobots]
	@term nvarchar(255) = '',
	@userId nvarchar(50) = '',
	@take int = 25,
	@skip int = 0
AS
BEGIN
	 SET NOCOUNT ON;

	 SELECT [Id]
		   ,[DailyCosts]
		   ,[CompanyName]
		   ,[RobotTypeName]
		   ,[Name]
		   ,[Icon]
		   ,[Photo]
		   ,[FirstName]
		   ,[LastName]
		   ,[Email]
		   ,[PhoneNumber]
		   ,(RobotRatingSum.RatingSum / RobotRatingCount.RatingCount) as AverageRating
	  INTO #temp
	  FROM [robot].[RobotView] as Robot
	  OUTER APPLY(SELECT SUM(RobotRating) as RatingSum FROM rental.Rentals WHERE RobotId = Robot.Id AND RobotRating > 0) As RobotRatingSum
	  OUTER APPLY(SELECT COUNT(*) as RatingCount FROM rental.Rentals WHERE RobotId = Robot.Id AND RobotRating > 0) As RobotRatingCount
	  WHERE 
	  UserId != @userId AND
	  (RobotRatingSum.RatingSum / RobotRatingCount.RatingCount) > 0 AND
	  [CompanyName] LIKE '%' + @term + '%' AND 
	  [RobotTypeName] LIKE '%' + @term + '%' AND 
	  [Name] LIKE '%' + @term + '%'

	  SELECT COUNT(*) FROM #temp

	  SELECT * FROM #temp
	  ORDER BY AverageRating desc
	  OFFSET @skip ROWS
		FETCH NEXT @take ROWS ONLY

	  SELECT r.RobotId, r.RobotRating, r.CustomerFeedback
	  FROM
	  (
	  	SELECT
	  		r.*,
	  		ROW_NUMBER() OVER(PARTITION BY r.[RobotId] ORDER BY r.[UpdatedAt] DESC) as RentalRank
	  	FROM rental.Rentals r
		WHERE RobotId IN (SELECT Id From #temp) AND RobotRating > 0
	  ) r
	  WHERE r.RentalRank <= 3
	  ORDER BY r.[UpdatedAt] DESC
END