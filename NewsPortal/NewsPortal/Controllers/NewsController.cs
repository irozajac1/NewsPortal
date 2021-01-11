using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Framework.Data.Entities;
using NewsPortal.Framework.Dtos.Request;
using NewsPortal.Framework.Interfaces;
using UserManagement.Framework.Helpers;
using AuthorizeAttribute = UserManagement.Framework.Helpers.AuthorizeAttribute;

namespace NewsPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsPortalService _newsService;

        public NewsController(INewsPortalService newsService)
        {
            _newsService = newsService;
        }
        //[AuthorizeAttribute(Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var news = await _newsService.GetNews();
            return Ok(news);
        }

        [AuthorizeAttribute(Roles.Admin)]
        [HttpPost("insert")]
        public async Task<IActionResult> InsertNews(NewsRequest request)
        {
            await _newsService.InsertNews(request);
            return Ok();
        }

        [AuthorizeAttribute(Roles.Admin)]
        [HttpPut("update")]
        public async Task<IActionResult> Update(News request)
        {
            try
            {
                await _newsService.Update(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[AuthorizeAttribute(Roles.Admin, Roles.User)]
        [HttpPost("search")]
        public async Task<IActionResult> Search(string request)
        {
            var news = await _newsService.SearchNews(request);
            return Ok(news);
        }
    }
}
