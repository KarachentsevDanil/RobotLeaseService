CREATE OR ALTER VIEW [robot].[ModelView] 
WITH SCHEMABINDING AS SELECT
  Model.Id,
  Model.[Name],
  Model.[Description],
  Model.TypeId,
  Model.CompanyId,
  Company.[Name] as CompanyName,
  Company.[Description] as CompanyDescription,
  RobotType.[Name] as RobotTypeName,
  RobotType.[Description] as RobotTypeDescription
  FROM [robot].[Models] Model
  INNER JOIN [robot].[Companies] Company ON Company.Id = Model.CompanyId
  INNER JOIN [robot].[Types] RobotType ON RobotType.Id = Model.TypeId
  GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex] ON [robot].[ModelView]
(
	[Id] ASC,
	[TypeId] ASC,
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE OR ALTER VIEW [rental].[RentalShortView] 
WITH SCHEMABINDING AS SELECT
  Rental.Id,
  Rental.RobotId,
  Robot.ModelId,
  Model.TypeId,
  Model.CompanyId
  FROM [rental].Rentals Rental
  INNER JOIN [robot].[Robots] Robot ON Rental.RobotId = Robot.Id
  INNER JOIN [robot].[Models] Model ON Model.Id = Robot.ModelId
  GO
  CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex] ON [rental].[RentalShortView]
(
	[Id] ASC,
	[RobotId] ASC,
	[ModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE OR ALTER VIEW [robot].[RobotShortView] 
WITH SCHEMABINDING AS SELECT
  Robot.Id,
  Robot.ModelId,
  Model.TypeId,
  Model.CompanyId
  FROM [robot].[Robots]	Robot
  INNER JOIN [robot].[Models] Model ON Model.Id = Robot.ModelId
  GO
  CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex] ON [robot].[RobotShortView]
(
	[Id] ASC,
	[ModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE OR ALTER VIEW [robot].[RobotView] 
WITH SCHEMABINDING AS SELECT
  Robot.Id,
  Robot.DailyCosts,
  Robot.ModelId,
  Robot.UserId,
  Robot.Icon,
  Robot.Photo,
  RobotOwner.FirstName,
  RobotOwner.LastName,
  RobotOwner.Email,
  RobotOwner.PhoneNumber,
  Model.[Name],
  Model.[Description],
  Model.TypeId,
  Model.CompanyId,
  Company.[Name] as CompanyName,
  Company.[Description] as CompanyDescription,
  RobotType.[Name] as RobotTypeName,
  RobotType.[Description] as RobotTypeDescription
  FROM [robot].[Robots]	Robot
  INNER JOIN [user].[Users] RobotOwner ON RobotOwner.Id = Robot.UserId
  INNER JOIN [robot].[Models] Model ON Model.Id = Robot.ModelId
  INNER JOIN [robot].[Companies] Company ON Company.Id = Model.CompanyId
  INNER JOIN [robot].[Types] RobotType ON RobotType.Id = Model.TypeId
  GO
  CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex] ON [robot].[RobotView]
(
	[Id] ASC,
	[UserId] ASC,
	[TypeId] ASC,
	[ModelId] ASC,
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE OR ALTER VIEW [robot].[ShortModelView] 
WITH SCHEMABINDING AS SELECT
  Model.Id,
  Model.CompanyId,
  Model.TypeId,
  Model.[Name],
  Company.[Name] as CompanyName
  FROM [robot].[Models] Model
  INNER JOIN [robot].[Companies] Company ON Company.Id = Model.CompanyId
  GO
  CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex] ON [robot].[ShortModelView]
(
	[Id] ASC,
	CompanyId ASC,
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE OR ALTER PROCEDURE [robot].[GetRobotCompany]
	@orderBy int = 1,
	@take int = 5
AS
BEGIN
	SET NOCOUNT ON;

     SELECT 
	   Company.[Id]
      ,Company.[Name]
	  ,RobotCount.RobotCount as RobotCount
	  ,RentalCount.RentalCount as RentalCount
    FROM [robot].[Companies] as Company
    
	OUTER APPLY ( 
		SELECT COUNT(*) as RobotCount
		FROM [robot].[RobotShortView] Robot WITH(NOEXPAND)
		WHERE Robot.CompanyId = Company.Id)
    AS RobotCount

    OUTER APPLY (
		SELECT COUNT(*) as RentalCount
		FROM [rental].[RentalShortView] Rental WITH(NOEXPAND)
		WHERE Company.Id = Rental.CompanyId)
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
GO
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
GO
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
GO
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
GO
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
	  ORDER BY AverageRating desc
	  OFFSET 0 ROWS
		FETCH NEXT @take ROWS ONLY
END
GO
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