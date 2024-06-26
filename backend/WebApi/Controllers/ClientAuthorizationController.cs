﻿using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.Client;
using Model.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/Client/Authorization")]
    public class ClientAuthorizationController : ControllerBase
    {
        private readonly IClientService _usersService;

        public ClientAuthorizationController(IClientService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("Register")]
        public ServiceResult<RegistrationResponse> Register([FromBody] RegistrationRequest<ClientRegistrationData> request)
        {
            return _usersService.Register(request);
        }


        [HttpPost("Login")]
        public ServiceResult<LoginResponse<ClientBasicData>> Login([FromBody] LoginRequest request)
        {
            var authorizationResult = _usersService.Login(request);

            return authorizationResult;
        }
    }
}
