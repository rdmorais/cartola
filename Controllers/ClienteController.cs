
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
    public class ClienteController : Controller
    {

        private ClienteService _clienteService = new ClienteService();

        [HttpGet]
        public IActionResult Get()
        {   
            try
            {
                return Ok(_clienteService.FindAll());
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
                var cliente = _clienteService.FindById(id);
                if (cliente == null) return NotFound();
                return Ok(cliente);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("~/api/cliente/post")]
        public IActionResult Post([FromBody]Cliente cliente)
        {
            var httpObject = _clienteService.Insert(cliente);

            
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
        [Route("~/api/cliente/put/{id}")]
        public IActionResult Put([FromBody]Cliente cliente, int id)
        {
            var httpObject = _clienteService.Update(cliente,id);
            
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
        [Route("~/api/cliente/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var httpObject = _clienteService.Delete(id);
            
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