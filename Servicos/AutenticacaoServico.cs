using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using api_sge.Database;
using api_sge.Entidades;
using api_sge.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api_sge.Servicos
{
    public class AutenticacaoServico : IAutenticacaoServico
    {
        private readonly DataContext _context;

        private readonly IConfiguration _configuration;

        public AutenticacaoServico(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Resposta<string>> Logar(string login, string password)
        {
            var resposta = new Resposta<string>();
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Login.ToLower().Equals(login.ToLower()));
            if (usuario == null)
            {
                resposta.Mensagem = "Login e/ou Senha inválido(s)!";
                return resposta;
            }

            if (!VerificarSenhaHash(password, usuario.SenhaHash, usuario.SenhaSalt))
            {
                resposta.Mensagem = "Login e/ou Senha inválido(s)!";
                return resposta;
            }

            resposta.Dados = GerarToken(usuario);
            return resposta;
        }

        public async Task<Resposta<long>> Registrar(Usuario usuario, string senha)
        {
            Resposta<long> resposta = new Resposta<long>();
            if (await VerificarUsuarioExiste(usuario.Login))
            {
                resposta.Mensagem = "Login indisponível!";
                return resposta;
            }

            CriarSenhaHash(senha, out byte[] senhaHash, out byte[] senhaSalt);
            usuario.SenhaHash = senhaHash;
            usuario.SenhaSalt = senhaSalt;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            
            resposta.Dados = usuario.UsuarioCodigo;
            return resposta;
        }

        public async Task<bool> VerificarUsuarioExiste(string login)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Login.ToLower().Equals(login.ToLower())))
            {
                return true;
            }

            return false;
        }

        private void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        private bool VerificarSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(senhaSalt))
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != senhaHash[i]){
                        return false;
                    }
                }

                return true;
            }
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioCodigo.ToString()),
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.Role, "Parceiro")
            };

            if (usuario.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}