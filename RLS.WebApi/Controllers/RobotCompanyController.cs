using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Companies;
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
            return StatusCode((int)HttpStatusCode.Created, Json(JsonResultData.Success(company)));
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

        [HttpGet("chart/robot")]
        public async Task<ActionResult> GetTopRobotCompaniesByRobotCountAsync(int? count)
        {
            var filterParams = new RobotPopularityFilterParamsDto
            {
                Type = RobotPopularity.ByRobotCount,
                CountToTake = count ?? 5
            };

            var companies = await _robotCompanyService.GetTopNPopularCompaniesAsync(filterParams);

            return Json(JsonResultData.Success(companies.Where(t => t.CountOfRobots > 0).Select(t => new PieChartModel
            {
                Name = t.Name,
                Value = t.CountOfRobots
            })));
        }

        [HttpGet("chart/rents")]
        public async Task<ActionResult> GetTopRobotCompaniesByRentsCountAsync(int? count)
        {
            var filterParams = new RobotPopularityFilterParamsDto
            {
                Type = RobotPopularity.ByRentCount,
                CountToTake = count ?? 5
            };

            var companies = await _robotCompanyService.GetTopNPopularCompaniesAsync(filterParams);

            return Json(JsonResultData.Success(companies.Where(t => t.CountOfRents > 0).Select(t => new PieChartModel
            {
                Name = t.Name,
                Value = t.CountOfRents
            })));
        }

        [HttpGet("bar-chart/all")]
        public async Task<ActionResult> GetTopRobotCompaniesByRobotAndRentsCountAsync(int? count)
        {
            var filterParams = new RobotPopularityFilterParamsDto
            {
                Type = RobotPopularity.ByRobotAndRentCount,
                CountToTake = count ?? 5
            };

            var companies = await _robotCompanyService.GetTopNPopularCompaniesAsync(filterParams);

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