﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Models;
using RLS.BLL.Services.Contracts.Robots;
using RLS.WebApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace RLS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RobotModelController : Controller
    {
        private readonly IRobotModelService _robotModelService;

        public RobotModelController(IRobotModelService robotModelService)
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
            return Json(JsonResultData.Success(model));
        }

        [HttpPost]
        public async Task<ActionResult> CreateRobotModelAsync([FromBody] CreateRobotModelDto robotModel)
        {
            var model = await _robotModelService.CreateRobotModelAsync(robotModel);
            return StatusCode((int) HttpStatusCode.Created, Json(JsonResultData.Success(model)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRobotModelAsync(int id, [FromBody] UpdateRobotModelDto robotModel)
        {
            robotModel.Id = id;
            var model = await _robotModelService.UpdateRobotModelAsync(robotModel);
            return Json(JsonResultData.Success(model));
        }

        [HttpDelete("model/{id}")]
        public async Task<ActionResult> GetModelsByTermAsync(int id)
        {
            var models = await _robotModelService.GetRobotModelsByTypeAsync(id);
            return Json(JsonResultData.Success(models));
        }
    }
}