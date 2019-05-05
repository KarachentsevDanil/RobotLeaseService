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