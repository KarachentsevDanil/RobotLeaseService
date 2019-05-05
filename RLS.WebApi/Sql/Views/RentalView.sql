CREATE OR ALTER VIEW [rental].[RentalView] 
WITH SCHEMABINDING AS SELECT
  Rental.Id,
  Rental.RobotId,
  Rental.UserId,
  Rental.[Status],
  Rental.OwnerFeedback,
  Rental.CustomerRating,
  Rental.RobotRating,
  Rental.CustomerFeedback,
  Rental.StartDate,
  Rental.EndDate,
  Robot.DailyCosts,
  Robot.ModelId,
  Robot.UserId as OwnerId,
  RobotOwner.FirstName as OwnerFirstName,
  RobotOwner.LastName as OwnerLastName,
  RobotOwner.Email as OwnerEmail,
  RobotOwner.PhoneNumber as OwnerPhoneCustomer,
  Customer.FirstName as CustomerFirstName,
  Customer.LastName as CustomerLastName,
  Customer.Email as CustomerEmail,
  Customer.PhoneNumber as CustomerPhone,
  Model.[Name] as ModelName,
  Model.[Description] as ModelDescription,
  Model.TypeId,
  Model.CompanyId,
  Company.[Name] as CompanyName,
  Company.[Description] as CompanyDescription,
  RobotType.[Name] as RobotTypeName,
  RobotType.[Description] as RobotTypeDescription
  FROM [rental].Rentals Rental
  INNER JOIN [user].[Users] Customer ON Customer.Id = Rental.UserId
  INNER JOIN [robot].[Robots] Robot ON Rental.RobotId = Robot.Id
  INNER JOIN [user].[Users] RobotOwner ON RobotOwner.Id = Robot.UserId
  INNER JOIN [robot].[Models] Model ON Model.Id = Robot.ModelId
  INNER JOIN [robot].[Companies] Company ON Company.Id = Model.CompanyId
  INNER JOIN [robot].[Types] RobotType ON RobotType.Id = Model.TypeId