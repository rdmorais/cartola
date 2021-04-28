using System;
using System.Net;
using System.Net.Http;
using ApiCliente.Domain.Models;
using ApiCliente.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiCliente.Controllers
{
    [Route("/api/[controller]")]
    [Authorize()]
    public class LogradouroController : Controller
    {
        
        private LogradouroService _logradouroService = new LogradouroService();

        [HttpGet]
        public IActionResult Get()
        {   
            try
            {
                return Ok(_logradouroService.FindAll());
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
                var cliente = _logradouroService.FindById(id);
                if (cliente == null) return NotFound();
                return Ok(cliente);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("~/api/logradouro/post")]
        public IActionResult Post([FromBody]Logradouro logradouro)
        {
            var httpObject = _logradouroService.Insert(logradouro);

            
            if (!httpObject.Sucesso)
            {
                return BadRequest(httpObject.Mensagem);
            }
            else
            {
                return Ok(httpObject.Mensagem);
            }
        }

        [HttpPut]
        [Route("~/api/logradouro/put/{id}")]
        public IActionResult Put([FromBody]Logradouro logradouro, int id)
        {
            var httpObject = _logradouroService.Update(logradouro,id);
            
            if (!httpObject.Sucesso)
            {
                return BadRequest(httpObject.Mensagem);
            }
            else
            {
                return Ok(httpObject.Mensagem);
            }
        }

        [HttpDelete]
        [Route("~/api/logradouro/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var httpObject = _logradouroService.Delete(id);
            
            if (!httpObject.Sucesso)
            {
                return BadRequest(httpObject.Mensagem);
            }
            else
            {
                return Ok(httpObject.Mensagem);
            }
        }
    }
}