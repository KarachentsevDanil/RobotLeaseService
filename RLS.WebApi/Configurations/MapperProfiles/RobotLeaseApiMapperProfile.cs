using AutoMapper;
using RLS.Domain.Enums;
using RLS.WebApi.Helpers.AutoMapper;
using RLS.WebApi.Models.Users;
using System.Collections.Generic;
using System.Security.Claims;

namespace RLS.WebApi.Configurations.MapperProfiles
{
    public class RobotLeaseApiMapperProfile : Profile
    {
        public RobotLeaseApiMapperProfile()
        {
            CreateMap<string, Role>().ConvertUsing<EnumTypeConverter<Role>>();

            CreateMap<Dictionary<string, string>, UserPrincipal>()
                .ForMember(
                    p => p.Id,
                    p => p.MapFrom(s => s.GetValueOrDefault(nameof(Domain.Users.User.Id))))

                .ForMember(
                    p => p.Email,
                    p => p.MapFrom(s => s.GetValueOrDefault(nameof(Domain.Users.User.Email))))

                .ForMember(
                    p => p.Interests,
                    p => p.MapFrom(s => s.GetValueOrDefault(nameof(Domain.Users.User.Interests))))

                .ForMember(
                    p => p.Role,
                    p => p.MapFrom(s =>
                        s.GetValueOrDefault(ClaimTypes.Role, Role.User.ToString())));
        }
    }
}