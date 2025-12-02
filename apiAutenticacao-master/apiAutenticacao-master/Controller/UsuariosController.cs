using apiAutenticacao.Data;
using apiAutenticacao.Models;
using apiAutenticacao.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt;
using apiAutenticacao.Services;
using apiAutenticacao.Models.Response;

namespace apiAutenticacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public UsuariosController(AppDbContext context, AuthService authservice) {

            _context = context;
            _authService = authservice;

        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuarioAsync([FromBody] CadastroUsuarioDTO dadosUsuario) {

            if (!ModelState.IsValid) {

                return BadRequest(ModelState);
            }

            ResponseCadastro response = await _authService.CadastrarUsuarioAsync(dadosUsuario);
            if (response.Erro) {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dadosUsuario) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseLogin response = await _authService.Login(dadosUsuario);

            if (response.Erro)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("alterarsenha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO dadosUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            var response = await _authService.AlterarSenhaAsync(dadosUsuario);

            if (response.Erro)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}