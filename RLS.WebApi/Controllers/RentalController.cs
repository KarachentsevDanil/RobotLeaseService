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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRentalByIdAsync(int id)
        {
            BuildUserPrincipal();
            var rental = await _rentalService.GetRentalAsync(id);
            return ValidateAccessToEntity(rental.Owner.Id, rental);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRentalAsync([FromBody] CreateRentalDto rental)
        {
            BuildUserPrincipal();
            rental.UserId = ApiUser.Id;

            var result = await _rentalService.CreateRentalAsync(rental);
            return StatusCode((int)HttpStatusCode.Created, Json(JsonResultData.Success(result)));
        }
    }
}