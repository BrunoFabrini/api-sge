using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_sge.Entidades;
using api_sge.Interfaces;
using api_sge.Modelos;
using api_sge.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace api_sge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoServico _autenticacaoServico;

        public AutenticacaoController(IAutenticacaoServico autenticacaoServico)
        {
            _autenticacaoServico = autenticacaoServico;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult<Resposta<long>>> Registrar(UsuarioRegistrarDto requisicao)
        {
            Usuario novoUsuario = new Usuario();
            novoUsuario.Login = requisicao.Login;
            novoUsuario.Admin = false;
            
            var resposta = await _autenticacaoServico.Registrar(novoUsuario, requisicao.Senha);
            if (!resposta.Sucesso)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }

        [HttpPost("Logar")]
        public async Task<ActionResult<Resposta<long>>> Logar(UsuarioLoginDto requisicao)
        {
            var resposta = await _autenticacaoServico.Logar(requisicao.Login, requisicao.Senha);
            if (!resposta.Sucesso)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }
    }
}