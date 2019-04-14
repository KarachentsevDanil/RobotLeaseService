﻿using AutoMapper;
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

namespace RLS.BLL.Configurations.MapperProfiles
{
    public class RobotLeaseMapperProfile : Profile
    {
        public RobotLeaseMapperProfile()
        {
            CreateMap<User, GetUserDto>()
                .ForMember(x => x.Role, t => t.MapFrom(p => p.Role.ToString()));

            CreateMap<RobotCompanyFilterParamsDto, RobotCompanyFilterParams>();

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

            CreateMap<CreateRobotDto, Robot>()
                .ForMember(x => x.Id, t => t.Ignore());

            CreateMap<UpdateRobotDto, Robot>();

            CreateMap<Robot, GetRobotDto>()
                .ForMember(x => x.UserId, t => t.MapFrom(p => p.User.Id))
                .ForMember(x => x.UserName, t => t.MapFrom(p => p.User.Email))
                .ForMember(x => x.CompanyName, t => t.MapFrom(p => p.Model.Company.Name))
                .ForMember(x => x.TypeName, t => t.MapFrom(p => p.Model.Type.Name))
                .ForMember(x => x.ModelName, t => t.MapFrom(p => p.Model.Name));

            CreateMap<Rental, GetRentalDto>()
                .ForMember(x => x.StartDate, t => t.MapFrom(p => p.StartDate.ToShortDateString()))
                .ForMember(x => x.EndDate, t => t.MapFrom(p => p.EndDate.ToShortDateString()))
                .ForMember(x => x.TotalPrice, t => t.MapFrom(p => (p.EndDate - p.StartDate).TotalDays * p.Robot.DailyCosts))
                .ForMember(x => x.Status, t => t.MapFrom(p => p.Status.ToString()))
                .ForMember(x => x.Owner, t => t.MapFrom(p => p.Robot.User))
                .ForMember(x => x.Customer, t => t.MapFrom(p => p.User));

            CreateMap<CreateRentalDto, Rental>()
                .ForMember(x => x.Id, t => t.Ignore())
                .ForMember(x => x.Status, t => t.Ignore());
        }
    }
}