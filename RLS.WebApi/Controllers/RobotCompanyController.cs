﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Companies;
using RLS.BLL.Services.Contracts.Robots;
using RLS.WebApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace RLS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RobotCompanyController : BaseApiController
    {
        private readonly IRobotCompanyService _robotCompanyService;

        public RobotCompanyController(IRobotCompanyService robotCompanyService, IMapper mapper) : base(mapper)
        {
            _robotCompanyService = robotCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCompaniesAsync([FromQuery] RobotCompanyFilterParamsDto filterParams)
        {
            var companies = await _robotCompanyService.GetCompaniesByFilterParamsAsync(filterParams);
            return Json(JsonResultData.Success(companies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCompanyByIdAsync(int id)
        {
            var company = await _robotCompanyService.GetRobotCompanyAsync(id);
            return NullEntityCheckResponse(company);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRobotCompanyAsync([FromBody] CreateRobotCompanyDto robotCompany)
        {
            var company = await _robotCompanyService.CreateRobotCompanyAsync(robotCompany);
            return StatusCode((int) HttpStatusCode.Created, Json(JsonResultData.Success(company)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRobotCompanyAsync(int id, [FromBody] UpdateRobotCompanyDto robotCompany)
        {
            robotCompany.Id = id;
            var company = await _robotCompanyService.UpdateRobotCompanyAsync(robotCompany);
            return Json(JsonResultData.Success(company));
        }

        [HttpGet("robot-companies")]
        public async Task<ActionResult> GetRobotCompaniesByTermAsync(string term)
        {
            var companies = await _robotCompanyService.GetRobotCompaniesByTermAsync(term ?? string.Empty);
            return Json(JsonResultData.Success(companies));
        }
    }
}