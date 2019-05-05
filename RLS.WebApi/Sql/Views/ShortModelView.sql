CREATE OR ALTER VIEW [robot].[ShortModelView] 
WITH SCHEMABINDING AS SELECT
  Model.Id,
  Model.CompanyId,
  Model.TypeId,
  Model.[Name],
  Company.[Name] as CompanyName
  FROM [robot].[Models] Model
  INNER JOIN [robot].[Companies] Company ON Company.Id = Model.CompanyId