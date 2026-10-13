using Microsoft.AspNetCore.Mvc;

namespace GestaoNailDesigner.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // POST: api/Login
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto loginDto)
        {
            if (loginDto.Email == "admin@naildesigner.com" && loginDto.Senha == "123456")
            {
                return Ok(new
                {
                    mensagem = "Login realizado com sucesso!",
                    token = "token-jwt-exemplo-seguro",
                    usuario = loginDto.Email
                });
            }

            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}