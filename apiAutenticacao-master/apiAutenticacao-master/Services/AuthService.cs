using apiAutenticacao.Models;
using apiAutenticacao.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using static BCrypt.Net.BCrypt;
using apiAutenticacao.Data;
using Microsoft.EntityFrameworkCore;
using apiAutenticacao.Models.Response;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace apiAutenticacao.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseLogin> Login(LoginDTO dadosUsuario)
        {

            Usuario? usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(Usuario => Usuario.Email == dadosUsuario.Email);
            if (usuarioEncontrado != null)
            {
                bool isValidPassword = Verify(dadosUsuario.Senha, usuarioEncontrado.Senha);
                if (!isValidPassword)
                {
                    return new ResponseLogin
                    {
                        Erro = false,
                        Mesage = "Login realizado com sucesso.",
                        Usuario = usuarioEncontrado
                    };
                }
                return new ResponseLogin
                {
                    Erro = true,
                    Mesage = "Login não realizado.",
                    Usuario = null
                };

            }
            return new ResponseLogin
            {
                Erro = true,
                Mesage = "Usuário não encontrado.",
            };
        }
        public async Task<ResponseCadastro> CadastrarUsuarioAsync(CadastroUsuarioDTO dadosUsuarioCadastro)
        {
            Usuario? usuarioExistente = await _context.Usuarios.
        FirstOrDefaultAsync(usuario => usuario.Email == dadosUsuarioCadastro.Email);

            if (usuarioExistente != null)
            {
                return new ResponseCadastro
                {
                    Erro = true,
                    Mesage = "Este email já está cadastrado.",
                    Usuario = null
                };

            }

            Usuario usuario = new Usuario
            {

                Nome = dadosUsuarioCadastro.Nome,
                Email = dadosUsuarioCadastro.Email,
                Senha = HashPassword(dadosUsuarioCadastro.Senha),
                ConfirmarSenha = HashPassword(dadosUsuarioCadastro.ConfirmarSenha)


            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new ResponseCadastro
            {

                Erro = false,
                Mesage = "Usuário criado com sucesso",
                Usuario = new Usuario
                {

                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email

                }

            };

        }
        public async Task<ResponseCadastro> AlterarSenhaAsync(AlterarSenhaDTO dadosAlterarSenha)
        {
            try
            {
                // busca o usuário
                Usuario? usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(usuario => usuario.Email == dadosAlterarSenha.Email);

                if (usuarioExistente == null)
                {
                    return new ResponseCadastro
                    {
                        Erro = true,
                        Mesage = "Este email não está cadastrado.",
                        Usuario = null
                    };
                }

                // verifica se o usuário sabe a senha atual
                bool senhaCorreta = Verify(dadosAlterarSenha.SenhaAtual, usuarioExistente.Senha);

                if (!senhaCorreta)
                {
                    return new ResponseCadastro
                    {
                        Erro = true,
                        Mesage = "Senha atual incorreta.",
                        Usuario = null
                    };
                }

                // nova senha Hash
                string novaSenhaHash = HashPassword(dadosAlterarSenha.NovaSenha);

                // atualiza o banco
                usuarioExistente.Senha = novaSenhaHash;

                _context.Usuarios.Update(usuarioExistente);
                await _context.SaveChangesAsync();

                // alteracao concluida
                return new ResponseCadastro
                {
                    Erro = false,
                    Mesage = "Senha alterada com sucesso!",
                    Usuario = usuarioExistente
                };
            }
            catch (Exception)
            {
                return new ResponseCadastro
                {
                    Erro = true,
                    Mesage = "Erro ao alterar senha: ",
                    Usuario = null

                };
            }
        }
    }
}

