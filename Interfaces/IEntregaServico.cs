using System.Collections.Generic;
using System.Threading.Tasks;
using api_sge.Modelos;
using api_sge.Servicos;

namespace api_sge.Interfaces
{
    public interface IEntregaServico
    {
        Task<Resposta<List<EntregaObterDto>>> ListarEntregas();

        Task<Resposta<EntregaObterDto>> ObterEntregaPorCodigo(long id);

        Task<Resposta<List<EntregaObterDto>>> InserirEntrega(EntregaInserirDto EntregaNova);
    
        Task<Resposta<EntregaObterDto>> AtualizarEntrega(EntregaAtualizarDto EntregaAtualizada);
        
        Task<Resposta<List<EntregaObterDto>>> ExcluirEntrega(long id);

        Task<Resposta<EntregaObterDto>> AtualizarLocalizacao(EntregaAtualizarLocalizacaoDto novaEntregaLocalizacao);
    }
}