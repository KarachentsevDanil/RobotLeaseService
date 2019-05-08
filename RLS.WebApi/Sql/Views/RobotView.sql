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