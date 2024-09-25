﻿using Core.Data.Dto.Account;
using Core.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/account/")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var userResponse = await _accountService.RegisterUserAsync(registerUserDto);

            return Ok(userResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var userResponse = await _accountService.LoginUserAsync(loginUserDto);

            return Ok(userResponse);
        }
    }
}
