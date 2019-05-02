using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Rents;
using RLS.BLL.DTOs.Rentals;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.Domain.Enums;
using RLS.WebApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace RLS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RentalController : BaseApiController
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService, IMapper mapper) : base(mapper)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult> GetRentalsAsync([FromQuery] RentalFilterParamsDto filterParams)
        {
            BuildUserPrincipal();

            if (ApiUser.Role == Role.User)
            {
                filterParams.UserId = ApiUser.Id;
            }

            var rentals = await _rentalService.GetRentalsByFilterParamsAsync(filterParams);
            return Json(JsonResultData.Success(rentals));
        }

        [HttpGet("calendar")]
        public async Task<ActionResult> GetRentalsForCalendarAsync(
            [FromQuery] RentalFilterParamsDto filterParams)
        {
            var rentals = await _rentalService.GetRentalsForCalendarByFilterParamsAsync(filterParams);
            return Json(rentals.Collection);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRentalByIdAsync(int id)
        {
            BuildUserPrincipal();
            var rental = await _rentalService.GetRentalAsync(id);
            return Json(JsonResultData.Success(rental));
        }

        [HttpPost]
        public async Task<ActionResult> CreateRentalAsync([FromBody] CreateRentalDto rental)
        {
            BuildUserPrincipal();
            rental.UserId = ApiUser.Id;

            var result = await _rentalService.CreateRentalAsync(rental);
            return StatusCode((int)HttpStatusCode.Created, Json(JsonResultData.Success(result)));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRentalAsync([FromBody] UpdateRentalDto rental)
        {
            var result = await _rentalService.UpdateRentalAsync(rental);
            return Json(JsonResultData.Success(result));
        }

        [HttpPut("customer")]
        public async Task<ActionResult> CustomerUpdateRentalAsync([FromBody] CustomerUpdateRentalDto rental)
        {
            var result = await _rentalService.CustomerUpdateRentalAsync(rental);
            return Json(JsonResultData.Success(result));
        }

        [HttpPut("owner")]
        public async Task<ActionResult> OwnerUpdateRentalAsync([FromBody] OwnerUpdateRentalDto rental)
        {
            var result = await _rentalService.OwnerUpdateRentalAsync(rental);
            return Json(JsonResultData.Success(result));
        }
    }
}