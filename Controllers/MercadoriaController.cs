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
    public class MercadoriaController : ControllerBase
    {
        private readonly IMercadoriaServico _mercadoriaServico;

        public MercadoriaController(IMercadoriaServico mercadoriaServico)
        {
            _mercadoriaServico = mercadoriaServico;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<Resposta<List<MercadoriaObterDto>>>> Listar()
        {
            return Ok(await _mercadoriaServico.ListarMercadorias());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resposta<MercadoriaObterDto>>> ObterPorCodigo(int id)
        {
            return Ok(await _mercadoriaServico.ObterMercadoriaPorCodigo(id));
        }

        [HttpPost]
        public async Task<ActionResult<Resposta<List<MercadoriaObterDto>>>> InserirMercadoria(MercadoriaInserirDto mercadoriaNova)
        {
            return Ok(await _mercadoriaServico.InserirMercadoria(mercadoriaNova));
        }

        [HttpPut]
        public async Task<ActionResult<Resposta<MercadoriaObterDto>>> Atualizar(MercadoriaAtualizarDto mercadoriaAtualizada)
        {
            var response = await _mercadoriaServico.AtualizarMercadoria(mercadoriaAtualizada);
            if (response.Dados == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<Resposta<List<MercadoriaObterDto>>>> Excluir(int id)
        {
            var response = await _mercadoriaServico.ExcluirMercadoria(id);
            if (response.Dados == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        // [HttpPost("Skill")]
        // public async Task<ActionResult<Resposta<List<MercadoriaObterDto>>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        // {
        //     return Ok(await _mercadoriaServico.AddCharacterSkill(newCharacterSkill));
        // }
    }
}