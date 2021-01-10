using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Framework.Dtos.Request;
using UserManagement.Framework.Entities;
using UserManagement.Framework.Helpers;
using UserManagement.Framework.Interfaces;
using AuthorizeAttribute = UserManagement.Framework.Helpers.AuthorizeAttribute;

namespace NewsPortal.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _userService.Authenticate(request);

            if (response == null)
            {
                return BadRequest("Neispravan unos pristupnih podataka! ");
            }

            return Ok(response);
        }

        [AuthorizeAttribute(Roles.Admin)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = _mapper.Map<User>(request);
            try
            {
                await _userService.Create(user, request.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [AuthorizeAttribute(Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            var result = _mapper.Map<IList<User>>(users);
            return Ok(result);
        }

        [AuthorizeAttribute(Roles.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            var result = _mapper.Map<User>(user);
            return Ok(result);
        }

        [AuthorizeAttribute(Roles.Admin)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.Id = request.Id;

            try
            {
                await _userService.Update(user, request.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
