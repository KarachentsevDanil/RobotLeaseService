using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots;
using RLS.BLL.Services.Contracts.Robots;
using RLS.BLL.Services.Contracts.Users;
using RLS.Domain.Enums;
using RLS.WebApi.Models;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RobotController : BaseApiController
    {
        private readonly IRobotService _robotService;

        private readonly IUserService _userService;

        private readonly IBackgroundJobClient _client;

        public RobotController(
            IRobotService robotService,
            IBackgroundJobClient client,
            IUserService userService,
            IMapper mapper) : base(mapper)
        {
            _robotService = robotService;
            _userService = userService;
            _client = client;
        }

        [HttpPost("list")]
        public async Task<ActionResult> GetRobotsAsync([FromBody] RobotFilterParamsDto filterParams)
        {
            BuildUserPrincipal();

            if (ApiUser.Role == Role.User)
            {
                filterParams.UserId = ApiUser.Id;
            }

            if (filterParams.FilterType == RobotFilterType.OnlyInterestedAt)
            {
                filterParams.UserInterests = ApiUser.Interests;
            }

            var robots = await _robotService.GetRobotsByFilterParamsAsync(filterParams);

            if (filterParams.FilterType == RobotFilterType.OnlyInterestedAt && !robots.Collection.Any())
            {
                _client.Enqueue(() => _userService.AddUserSearchResultAsync(ApiUser.Id, CancellationToken.None));
            }

            return Json(JsonResultData.Success(robots));
        }

        [HttpPost("dashboard")]
        public async Task<ActionResult> GetDashboardRobotsAsync([FromBody] RobotFilterParamsDto filterParams)
        {
            BuildUserPrincipal();

            filterParams.UserId = ApiUser.Id;

            var robots = await _robotService.GetDashboardRobotByFilterParamsAsync(filterParams);
            return Json(JsonResultData.Success(robots));
        }

        [HttpPost("valuable")]
        public async Task<ActionResult> GetMostValuableRobotsAsync([FromBody] RobotMostValuableFilterParamsDto filterParams)
        {
            BuildUserPrincipal();

            filterParams.UserId = ApiUser.Id;

            var robots = await _robotService.GetMostValuableRobotByFilterParamsAsync(filterParams);

            return Json(JsonResultData.Success(robots.Where(t => t.Id != filterParams.CurrentRobotId)));
        }

        [HttpGet("statistic")]
        public async Task<ActionResult> GetMostValuableRobotsAsync()
        {
            var result = await _robotService.GetDashboardStatisticModelAsync();
            return Json(JsonResultData.Success(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRobotByIdAsync(int id)
        {
            var robot = await _robotService.GetRobotAsync(id);
            return NullEntityCheckResponse(robot);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRobotAsync([FromBody] CreateRobotDto robot)
        {
            BuildUserPrincipal();
            robot.UserId = ApiUser.Id;

            var result = await _robotService.CreateRobotAsync(robot);

            _client.Enqueue(() => _robotService.SendNotificationByUserInterestsAsync(CancellationToken.None));

            return StatusCode((int)HttpStatusCode.Created, Json(JsonResultData.Success(result)));
        }

        [HttpPost("favorite/{id}")]
        public async Task<ActionResult> CreateFavoriteUserRobotAsync(int id)
        {
            BuildUserPrincipal();

            await _robotService.CreateFavoriteUserRobotAsync(ApiUser.Id, id);

            return StatusCode((int)HttpStatusCode.Created, Json(JsonResultData.Success()));
        }

        [HttpDelete("favorite/{id}")]
        public async Task<ActionResult> DeleteFavoriteUserRobotAsync(int id)
        {
            BuildUserPrincipal();

            await _robotService.DeleteFavoriteUserRobotAsync(ApiUser.Id, id);

            return StatusCode((int)HttpStatusCode.Created, Json(JsonResultData.Success()));
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