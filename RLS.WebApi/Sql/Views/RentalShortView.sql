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