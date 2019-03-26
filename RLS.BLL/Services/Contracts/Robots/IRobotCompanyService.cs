﻿using RLS.BLL.DTOs.Robots;
using RLS.BLL.DTOs.Robots.Companies;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Robots
{
    public interface IRobotCompanyService
    {
        Task<GetRobotDto> CreateRobotCompanyAsync(CreateRobotCompanyDto item, CancellationToken ct = default);

        Task<GetRobotDto> UpdateRobotCompanyAsync(UpdateRobotCompanyDto item, CancellationToken ct = default);

        Task<GetRobotDto> GetRobotCompanyAsync(int id, CancellationToken ct = default);
    }
}