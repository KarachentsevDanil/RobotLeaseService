using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RLS.Domain.Enums;
using RLS.WebApi.Extensions;
using RLS.WebApi.Models;
using RLS.WebApi.Models.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace RLS.WebApi.Controllers
{
    public class BaseApiController : Controller
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        private readonly IMapper _mapper;

        protected UserPrincipal ApiUser { get; set; }

        public BaseApiController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ActionResult NullEntityCheckResponse<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                return NotFound();
            }

            return Json(JsonResultData.Success(entity));
        }

        public ActionResult ValidateAccessToEntity<TEntity>(string userId, TEntity entity)
        {
            if (entity == null)
            {
                return NotFound();
            }

            if (ApiUser.Role == Role.Admin)
            {
                return Json(JsonResultData.Success(entity));
            }

            if (userId != ApiUser.Id)
            {
                return Forbid();
            }

            return Json(JsonResultData.Success(entity));
        }

        private void BuildUserPrincipal()
        {
            if (ApiUser == null)
            {
                string rawToken = HttpContext.Request.GetBearerToken();

                if (!string.IsNullOrWhiteSpace(rawToken))
                {
                    UserPrincipal user = Map(rawToken);

                    ApiUser = user;
                }
                else
                {
                    throw new UnauthorizedAccessException("Token is empty.");
                }
            }
        }

        private UserPrincipal Map(string rawToken)
        {
            if (!string.IsNullOrWhiteSpace(rawToken))
            {
                try
                {
                    JwtSecurityToken jwtToken = _tokenHandler.ReadJwtToken(rawToken);

                    Dictionary<string, string> claims = jwtToken.Claims.ToDictionary(
                        c => c.Type,
                        c => c.Value);

                    return _mapper.Map<UserPrincipal>(claims);
                }
                catch (Exception exp)
                {
                    throw new UnauthorizedAccessException("Error during mapping jwt token", exp);
                }
            }

            throw new UnauthorizedAccessException("Jwt token is null");
        }
    }
}