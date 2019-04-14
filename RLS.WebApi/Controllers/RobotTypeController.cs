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
    }
}