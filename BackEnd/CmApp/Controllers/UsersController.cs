﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CmApp.Contracts;
using CmApp.Domains;
using CmApp.Entities;
using CmApp.Repositories;
using CmApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CmApp.Controllers
{
    [Route("api/users")]
    [Authorize(Roles = "user, admin", AuthenticationSchemes = "user, admin")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService UserService = new UserService()
        {
            UserRepository = new UserRepository()
        };

        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        [HttpGet("{userId}")]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var authUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var role = HttpContext.User.FindFirst(ClaimTypes.Role).Value;

                if (authUserId != userId && role != "admin")
                    throw new Exception("You can not access this resource");

                var user = await UserService.GetSelectedUser(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                var newUser = await UserService.InsertNewUser(user);

                var userDetails = new UserDetails
                {
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    Currency = newUser.Currency
                };

                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Users/5
        [HttpPut("{userId}")]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Put(string userId, [FromBody] UserDetails user)
        {
            try
            {
                var authUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var role = HttpContext.User.FindFirst(ClaimTypes.Role).Value;

                if (authUserId != userId && role != "admin")
                    throw new Exception("You can not access this resource");

                await UserService.UpdateUserDetails(userId, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
