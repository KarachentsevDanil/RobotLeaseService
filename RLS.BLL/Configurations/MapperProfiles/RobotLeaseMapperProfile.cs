using AutoMapper;
using RLS.BLL.DTOs.Users;
using RLS.Domain.Users;

namespace RLS.BLL.Configurations.MapperProfiles
{
    public class RobotLeaseMapperProfile : Profile
    {
        public RobotLeaseMapperProfile()
        {
            CreateMap<User, GetUserDto>()
                .ForMember(x => x.Role, t => t.MapFrom(p => p.Role.ToString()));
        }
    }
}