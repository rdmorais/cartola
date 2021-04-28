using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiCliente.Repositories;
using ApiCliente.Services;
using ApiCliente.Domain.Models;

namespace ApiCliente.Controllers
{
    [Route("/api/[controller]")]
    public class TokenController : Controller
    {
        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate([FromBody]User model)
        {
            // Recupera o usuário
            var user = UserRepository.Get(model.Username, model.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
    }
}