CREATE OR ALTER PROCEDURE [robot].[GetRobotType]
	@orderBy int = 1,
	@take int = 5
AS
BEGIN
	SET NOCOUNT ON;

     SELECT 
	   RobotType.[Id]
      ,RobotType.[Name]
	  ,RobotCount.RobotCount as RobotCount
	  ,RentalCount.RentalCount as RentalCount
    FROM [robot].[Types] as RobotType
    
	OUTER APPLY ( 
		SELECT COUNT(*) as RobotCount
		FROM [robot].[RobotShortView] Robot WITH(NOEXPAND)
		WHERE Robot.TypeId = RobotType.Id)
    AS RobotCount

    OUTER APPLY (
		SELECT COUNT(*) as RentalCount
		FROM [rental].[RentalShortView] Rental WITH(NOEXPAND)
		WHERE RobotType.Id = Rental.TypeId)
    AS RentalCount

	ORDER BY CASE @orderBy
	WHEN 1 THEN RobotCount
	WHEN 2 THEN RentalCount
	WHEN 3 THEN RobotCount + RentalCount
	END 
	DESC
	OFFSET 0 ROWS
		FETCH NEXT @take ROWS ONLY	
END