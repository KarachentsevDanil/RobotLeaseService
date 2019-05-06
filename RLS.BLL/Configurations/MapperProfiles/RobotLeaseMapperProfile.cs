using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Rents;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Rentals;
using RLS.BLL.DTOs.Robots;
using RLS.BLL.DTOs.Robots.Companies;
using RLS.BLL.DTOs.Robots.Models;
using RLS.BLL.DTOs.Robots.Types;
using RLS.BLL.DTOs.Users;
using RLS.Domain.FilterParams.Rents;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Rentals;
using RLS.Domain.Robots;
using RLS.Domain.Users;
using System;
using System.Linq;
using RLS.Domain.Models.Robots;

namespace RLS.BLL.Configurations.MapperProfiles
{
    public class RobotLeaseMapperProfile : Profile
    {
        public RobotLeaseMapperProfile()
        {
            CreateMap<User, GetUserDto>()
                .ForMember(x => x.Role, t => t.MapFrom(p => p.Role.ToString()));

            CreateMap<RobotCompanyFilterParamsDto, RobotCompanyFilterParams>();

            CreateMap<RobotPopularityFilterParamsDto, RobotPopularityFilterParams>();

            CreateMap<RobotFilterParamsDto, RobotFilterParams>();

            CreateMap<RobotModelFilterParamsDto, RobotModelFilterParams>();

            CreateMap<RobotTypeFilterParamsDto, RobotTypeFilterParams>();

            CreateMap<RentalFilterParamsDto, RentalFilterParams>();

            CreateMap<CreateRobotCompanyDto, RobotCompany>()
                .ForMember(x => x.Id, t => t.Ignore());

            CreateMap<UpdateRobotCompanyDto, RobotCompany>();

            CreateMap<RobotCompany, GetRobotCompanyDto>();

            CreateMap<CreateRobotTypeDto, RobotType>()
                .ForMember(x => x.Id, t => t.Ignore());

            CreateMap<UpdateRobotTypeDto, RobotType>();

            CreateMap<RobotType, GetRobotTypeDto>();

            CreateMap<CreateRobotModelDto, RobotModel>()
                .ForMember(x => x.Id, t => t.Ignore());

            CreateMap<UpdateRobotModelDto, RobotModel>();

            CreateMap<RobotModel, GetRobotModelDto>()
                .ForMember(x => x.CompanyName, t => t.MapFrom(p => p.Company.Name))
                .ForMember(x => x.TypeName, t => t.MapFrom(p => p.Type.Name));

            CreateMap<RobotModelChartModel, GetRobotModelPopularityDto>()
                .ForMember(x => x.Title, t => t.MapFrom(p => $"{p.CompanyName} {p.Name}"))
                .ForMember(x => x.CountOfRobots, t => t.MapFrom(p => p.RobotCount))
                .ForMember(x => x.CountOfRents, t => t.MapFrom(p => p.RentalCount));

            CreateMap<RobotCompanyChartModel, GetRobotCompanyPopularityDto>()
                .ForMember(x => x.CountOfRobots, t => t.MapFrom(p => p.RobotCount))
                .ForMember(x => x.CountOfRents, t => t.MapFrom(p => p.RentalCount));

            CreateMap<RobotTypeChartModel, GetRobotTypePopularityDto>()
                .ForMember(x => x.CountOfRobots, t => t.MapFrom(p => p.RobotCount))
                .ForMember(x => x.CountOfRents, t => t.MapFrom(p => p.RentalCount));

            CreateMap<CreateRobotDto, Robot>()
                .ForMember(x => x.Id, t => t.Ignore());

            CreateMap<UpdateRobotDto, Robot>();

            CreateMap<Robot, GetRobotDto>()
                .ForMember(x => x.UserId, t => t.MapFrom(p => p.User.Id))
                .ForMember(x => x.UserName, t => t.MapFrom(p => p.User.Email))
                .ForMember(x => x.UserPhone, t => t.MapFrom(p => p.User.PhoneNumber))
                .ForMember(x => x.UserFullName, t => t.MapFrom(p => $"{p.User.FirstName} {p.User.LastName}"))
                .ForMember(x => x.CompanyId, t => t.MapFrom(p => p.Model.CompanyId))
                .ForMember(x => x.CompanyName, t => t.MapFrom(p => p.Model.Company.Name))
                .ForMember(x => x.TypeId, t => t.MapFrom(p => p.Model.TypeId))
                .ForMember(x => x.TypeName, t => t.MapFrom(p => p.Model.Type.Name))
                .ForMember(x => x.Description, t => t.MapFrom(p => p.Model.Description))
                .ForMember(x => x.ModelId, t => t.MapFrom(p => p.ModelId))
                .ForMember(x => x.ModelName, t => t.MapFrom(p => p.Model.Name))
                .ForMember(x => x.AvarageRating, t => t.MapFrom(p =>
                    p.Rentals != null && p.Rentals.Any(r => r.RobotRating > 0)
                        ? p.Rentals.Where(r => r.RobotRating > 0).Average(x => x.RobotRating)
                        : 0));

            CreateMap<Rental, GetShortRobotRentalDto>()
                .ForMember(x => x.Customer, t => t.MapFrom(p => p.User))
                .ForMember(x => x.StartDate, t => t.MapFrom(p => p.StartDate.ToString("F")))
                .ForMember(x => x.EndDate, t => t.MapFrom(p => p.EndDate.ToString("F")))
                .ForMember(x => x.Status, t => t.MapFrom(p => p.Status.ToString()));

            CreateMap<Rental, GetRentalDto>()
                .ForMember(x => x.StartDate, t => t.MapFrom(p => p.StartDate.ToString("F")))
                .ForMember(x => x.EndDate, t => t.MapFrom(p => p.EndDate.ToString("F")))
                .ForMember(x => x.TotalPrice, t => t.MapFrom(p => (p.EndDate - p.StartDate).TotalDays * p.Robot.DailyCosts))
                .ForMember(x => x.Status, t => t.MapFrom(p => p.Status.ToString()))
                .ForMember(x => x.Messages, t => t.MapFrom(p => p.Messages.OrderByDescending(m => m.CreatedAt).ToList()))
                .ForMember(x => x.Owner, t => t.MapFrom(p => p.Robot.User))
                .ForMember(x => x.Customer, t => t.MapFrom(p => p.User));

            CreateMap<CreateRentalDto, Rental>()
                .ForMember(x => x.Id, t => t.Ignore())
                .ForMember(x => x.Status, t => t.Ignore());

            CreateMap<CreateRentalMessageDto, RentalMessage>();

            CreateMap<CustomerUpdateRentalDto, Rental>();

            CreateMap<OwnerUpdateRentalDto, Rental>();

            CreateMap<UpdateRentalDto, Rental>();

            CreateMap<RentalMessage, GetRentalMessageDto>()
                .ForMember(x => x.CreatedAt, t => t.MapFrom(p => p.CreatedAt.ToString("F")));

            CreateMap<Rental, GetRentalForCalendarDto>()
                .ForMember(x => x.Title, t => t.MapFrom(p => $"{p.User.FirstName} {p.User.LastName} - {p.User.Email}"))
                .ForMember(x => x.Start, t => t.MapFrom(p => p.StartDate.ToString("O")))
                .ForMember(x => x.End, t => t.MapFrom(p => p.EndDate.ToString("O")))
                .ForMember(x => x.Color, t => t.MapFrom(p => DateTime.UtcNow > p.EndDate ? "#607D8B" : "#2196F3"));
        }
    }
}