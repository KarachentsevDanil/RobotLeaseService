CREATE OR ALTER PROCEDURE [robot].[GetRobotModel]
	@companyId int = null,
	@typeId int = null,
	@orderBy int = 1,
	@take int = 5
AS
BEGIN
	SET NOCOUNT ON;

     SELECT 
	   Model.[Id]
      ,Model.[Name]
	  ,Model.CompanyName
	  ,RobotCount.RobotCount as RobotCount
	  ,RentalCount.RentalCount as RentalCount
    FROM [robot].[ShortModelView] as Model
    
	OUTER APPLY ( 
		SELECT COUNT(*) as RobotCount
		FROM [robot].[Robots] Robot
		WHERE Robot.ModelId = Model.Id)
    AS RobotCount

    OUTER APPLY (
		SELECT COUNT(*) as RentalCount
		FROM [rental].[RentalShortView] Rental WITH(NOEXPAND)
		WHERE Model.Id = Rental.ModelId)
    AS RentalCount

	WHERE Model.TypeId = ISNULL(@typeId, Model.TypeId) AND Model.CompanyId = ISNULL(@companyId, Model.CompanyId)

	ORDER BY CASE @orderBy
	WHEN 1 THEN RobotCount
	WHEN 2 THEN RentalCount
	WHEN 3 THEN RobotCount + RentalCount
	END
	DESC
	OFFSET 0 ROWS
		FETCH NEXT @take ROWS ONLY	
END
