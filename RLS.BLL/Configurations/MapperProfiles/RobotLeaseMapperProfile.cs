using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Rents;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots;
using RLS.BLL.DTOs.Robots.Companies;
using RLS.BLL.DTOs.Robots.Models;
using RLS.BLL.DTOs.Robots.Types;
using RLS.BLL.DTOs.Users;
using RLS.Domain.FilterParams.Rents;
using RLS.Domain.FilterParams.Robots;
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
                .ForMember(x => x.Company, t => t.MapFrom(p => p.Company))
                .ForMember(x => x.Type, t => t.MapFrom(p => p.Type));

            CreateMap<CreateRobotDto, Robot>()
                .ForMember(x => x.Id, t => t.Ignore());

            CreateMap<UpdateRobotDto, Robot>();

            CreateMap<Robot, GetRobotDto>()
                .ForMember(x => x.Model, t => t.MapFrom(p => p.Model));
        }
    }
}