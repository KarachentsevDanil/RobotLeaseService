using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Models;
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
    public class RobotModelController : BaseApiController
    {
        private readonly IRobotModelService _robotModelService;

        public RobotModelController(IRobotModelService robotModelService, IMapper mapper) : base(mapper)
        {
            _robotModelService = robotModelService;
        }

        [HttpGet]
        public async Task<ActionResult> GetModelsAsync([FromQuery] RobotModelFilterParamsDto filterParams)
        {
            var models = await _robotModelService.GetModelsByFilterParamsAsync(filterParams);
            return Json(JsonResultData.Success(models));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetModelByIdAsync(int id)
        {
            var model = await _robotModelService.GetRobotModelAsync(id);
            return NullEntityCheckResponse(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRobotModelAsync([FromBody] CreateRobotModelDto robotModel)
        {
            var model = await _robotModelService.CreateRobotModelAsync(robotModel);
            return StatusCode((int)HttpStatusCode.Created, Json(JsonResultData.Success(model)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRobotModelAsync(int id, [FromBody] UpdateRobotModelDto robotModel)
        {
            robotModel.Id = id;
            var model = await _robotModelService.UpdateRobotModelAsync(robotModel);
            return Json(JsonResultData.Success(model));
        }

        [HttpGet("models")]
        public async Task<ActionResult> GetModelsByTermAsync()
        {
            var models = await _robotModelService.GetRobotModelsAsync();
            return Json(JsonResultData.Success(models));
        }

        [HttpGet("chart/robot")]
        public async Task<ActionResult> GetTopRobotModelsByRobotCountAsync(int? count)
        {
            var filterParams = new RobotPopularityFilterParamsDto
            {
                Type = RobotPopularity.ByRobotCount,
                CountToTake = count ?? 5
            };

            var companies = await _robotModelService.GetTopNPopularModelsAsync(filterParams);

            return Json(JsonResultData.Success(companies.Where(t => t.CountOfRobots > 0).Select(t => new BarChartModel
            {
                Name = t.Title,
                Value = t.CountOfRobots
            })));
        }

        [HttpGet("chart/rents")]
        public async Task<ActionResult> GetTopRobotModelsByRentsCountAsync(int? count)
        {
            var filterParams = new RobotPopularityFilterParamsDto
            {
                Type = RobotPopularity.ByRentCount,
                CountToTake = count ?? 5
            };

            var companies = await _robotModelService.GetTopNPopularModelsAsync(filterParams);

            return Json(JsonResultData.Success(companies.Where(t => t.CountOfRents > 0).Select(t => new BarChartModel
            {
                Name = t.Title,
                Value = t.CountOfRents
            })));
        }
    }
}