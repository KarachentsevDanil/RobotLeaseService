using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots;
using RLS.BLL.Services.Contracts.Robots;
using RLS.Domain.Enums;
using RLS.WebApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace RLS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RobotController : BaseApiController
    {
        private readonly IRobotService _robotService;

        public RobotController(IRobotService robotService, IMapper mapper) : base(mapper)
        {
            _robotService = robotService;
        }

        [HttpGet]
        public async Task<ActionResult> GetRobotsAsync([FromQuery] RobotFilterParamsDto filterParams)
        {
            BuildUserPrincipal();

            if (ApiUser.Role == Role.User)
            {
                filterParams.UserId = ApiUser.Id;
            }

            var robots = await _robotService.GetRobotsByFilterParamsAsync(filterParams);
            return Json(JsonResultData.Success(robots));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRobotByIdAsync(int id)
        {
            BuildUserPrincipal();
            var robot = await _robotService.GetRobotAsync(id);
            return ValidateAccessToEntity(robot.UserId, robot);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRobotAsync([FromBody] CreateRobotDto robot)
        {
            BuildUserPrincipal();
            robot.UserId = ApiUser.Id;

            var result = await _robotService.CreateRobotAsync(robot);
            return StatusCode((int) HttpStatusCode.Created, Json(JsonResultData.Success(result)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRobotAsync(int id, [FromBody] UpdateRobotDto robot)
        {
            robot.Id = id;
            var result = await _robotService.UpdateRobotAsync(robot);
            return Json(JsonResultData.Success(result));
        }
    }
}