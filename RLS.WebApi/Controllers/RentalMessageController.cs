using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLS.BLL.DTOs.Rentals;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.WebApi.Models;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RLS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RentalMessageController : BaseApiController
    {
        private readonly IRentalMessageService _rentalMessageService;

        private readonly IEmailSender _emailSender;

        private readonly IBackgroundJobClient _backgroundJobClient;

        public RentalMessageController(
            IRentalMessageService rentalMessageService,
            IEmailSender emailSender,
            IBackgroundJobClient backgroundJobClient,
            IMapper mapper) : base(mapper)
        {
            _emailSender = emailSender;
            _rentalMessageService = rentalMessageService;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRentalMessageAsync([FromBody] CreateRentalMessageDto message)
        {
            BuildUserPrincipal();

            message.UserId = ApiUser.Id;

            var result = await _rentalMessageService.CreateRentalMessageAsync(message);

            _backgroundJobClient.Enqueue(() => _rentalMessageService.SendEmailAsync(message, CancellationToken.None));

            return StatusCode((int)HttpStatusCode.Created, Json(JsonResultData.Success(result)));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRentalMessagesAsync([FromBody] UpdateRentalMessagesDto items)
        {
            await _rentalMessageService.UpdateRentalMessagesAsync(items);
            return Ok();
        }
    }
}