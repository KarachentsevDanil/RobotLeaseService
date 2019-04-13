using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.FilterParams.Rents;
using RLS.BLL.DTOs.Rentals;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.WebApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace RLS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult> GetRentalsAsync([FromQuery] RentalFilterParamsDto filterParams)
        {
            var rentals = await _rentalService.GetRentalsByFilterParamsAsync(filterParams);
            return Json(JsonResultData.Success(rentals));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRentalByIdAsync(int id)
        {
            var rental = await _rentalService.GetRentalAsync(id);
            return Json(JsonResultData.Success(rental));
        }

        [HttpPost]
        public async Task<ActionResult> CreateRentalAsync([FromBody] CreateRentalDto rental)
        {
            var result = await _rentalService.CreateRentalAsync(rental);
            return StatusCode((int) HttpStatusCode.Created, Json(JsonResultData.Success(result)));
        }
    }
}