
using System;
using ApiFantasy.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiFantasy.Service.Services;

namespace ApiFantasy.Application.Controllers
{
    [Route("/api/[controller]")]
    [Authorize()]
    public class UserController : Controller
    {

        private UserService _userService = new UserService();

        [HttpGet]
        public IActionResult Get()
        {   
            try
            {
                return Ok(_userService.GetAll());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}