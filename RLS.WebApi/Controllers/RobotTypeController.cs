﻿using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Types;
using RLS.BLL.Services.Contracts.Robots;
using RLS.WebApi.Models;
using System.Net;
using System.Threading.Tasks;
using RLS.Domain.Enums;
using RLS.WebApi.Models.Charts;

namespace RLS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RobotTypeController : BaseApiController
    {
        private readonly IRobotTypeService _robotTypeService;

        public RobotTypeController(IRobotTypeService robotTypeService, IMapper mapper) : base(mapper)
        {
            _robotTypeService = robotTypeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetTypesAsync([FromQuery] RobotTypeFilterParamsDto filterParams)
        {
            var types = await _robotTypeService.GetRobotTypesByFilterParamsAsync(filterParams);
            return Json(JsonResultData.Success(types));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTypeByIdAsync(int id)
        {
            var type = await _robotTypeService.GetRobotTypeAsync(id);
            return NullEntityCheckResponse(type);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRobotTypeAsync([FromBody] CreateRobotTypeDto robotType)
        {
            var type = await _robotTypeService.CreateRobotTypeAsync(robotType);
            return StatusCode((int) HttpStatusCode.Created, Json(JsonResultData.Success(type)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRobotTypeAsync(int id, [FromBody] UpdateRobotTypeDto robotType)
        {
            robotType.Id = id;
            var type = await _robotTypeService.UpdateRobotTypeAsync(robotType);
            return Json(JsonResultData.Success(type));
        }

        [HttpGet("robot-types")]
        public async Task<ActionResult> GetRobotTypesByTermAsync(string term)
        {
            var types = await _robotTypeService.GetRobotTypesByTermAsync(term ?? string.Empty);
            return Json(JsonResultData.Success(types));
        }

        [HttpGet("chart/robot")]
        public async Task<ActionResult> GetTopRobotTypesByRobotCountAsync(int? count)
        {
            var filterParams = new RobotPopularityFilterParamsDto
            {
                Type = RobotPopularity.ByRobotCount,
                CountToTake = count ?? 5
            };

            var companies = await _robotTypeService.GetTopNPopularTypesAsync(filterParams);

            return Json(JsonResultData.Success(companies.Where(t => t.CountOfRobots > 0).Select(t => new PieChartModel
            {
                Name = t.Name,
                Value = t.CountOfRobots
            })));
        }

        [HttpGet("chart/rents")]
        public async Task<ActionResult> GetTopRobotTypesByRentsCountAsync(int? count)
        {
            var filterParams = new RobotPopularityFilterParamsDto
            {
                Type = RobotPopularity.ByRentCount,
                CountToTake = count ?? 5
            };

            var companies = await _robotTypeService.GetTopNPopularTypesAsync(filterParams);

            return Json(JsonResultData.Success(companies.Where(t => t.CountOfRents > 0).Select(t => new PieChartModel
            {
                Name = t.Name,
                Value = t.CountOfRents
            })));
        }

        [HttpGet("bar-chart/all")]
        public async Task<ActionResult> GetTopRobotTypesByRobotAndRentsCountAsync(int? count)
        {
            var filterParams = new RobotPopularityFilterParamsDto
            {
                Type = RobotPopularity.ByRobotAndRentCount,
                CountToTake = count ?? 5
            };

            var companies = await _robotTypeService.GetTopNPopularTypesAsync(filterParams);

            var response = new BarChartModel()
            {
                Titles = companies.Select(t => t.Name),
                RobotRentsCount = companies.Select(t => t.CountOfRents),
                RobotsCount = companies.Select(t => t.CountOfRobots)
            };

            return Json(JsonResultData.Success(response));
        }
    }
}