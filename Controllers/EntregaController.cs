using System.Collections.Generic;
using System.Threading.Tasks;
using api_sge.Interfaces;
using api_sge.Modelos;
using api_sge.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_sge.Controllers
{
    [Authorize(Roles="Admin, Parceiro")]
    [ApiController]
    [Route("[controller]")]
    public class EntregaController : ControllerBase
    {
        private readonly IEntregaServico _EntregaServico;

        public EntregaController(IEntregaServico EntregaServico)
        {
            _EntregaServico = EntregaServico;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<Resposta<List<EntregaObterDto>>>> Listar()
        {
            return Ok(await _EntregaServico.ListarEntregas());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resposta<EntregaObterDto>>> ObterPorCodigo(int id)
        {
            return Ok(await _EntregaServico.ObterEntregaPorCodigo(id));
        }

        [HttpPost]
        public async Task<ActionResult<Resposta<List<EntregaObterDto>>>> InserirEntrega(EntregaInserirDto EntregaNova)
        {
            return Ok(await _EntregaServico.InserirEntrega(EntregaNova));
        }

        [HttpPut]
        public async Task<ActionResult<Resposta<EntregaObterDto>>> Atualizar(EntregaAtualizarDto EntregaAtualizada)
        {
            var response = await _EntregaServico.AtualizarEntrega(EntregaAtualizada);
            if (response.Dados == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<Resposta<List<EntregaObterDto>>>> Excluir(int id)
        {
            var response = await _EntregaServico.ExcluirEntrega(id);
            if (response.Dados == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpPost("Localizacao")]
        public async Task<ActionResult<Resposta<List<EntregaObterDto>>>> AtualizarLocalizacao(EntregaAtualizarLocalizacaoDto novaLocalizacao)
        {
            return Ok(await _EntregaServico.AtualizarLocalizacao(novaLocalizacao));
        }
    }
}