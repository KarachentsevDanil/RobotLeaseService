CREATE OR ALTER VIEW [robot].[RobotShortView] 
WITH SCHEMABINDING AS SELECT
  Robot.Id,
  Robot.ModelId,
  Model.TypeId,
  Model.CompanyId
  FROM [robot].[Robots]	Robot
  INNER JOIN [robot].[Models] Model ON Model.Id = Robot.ModelId