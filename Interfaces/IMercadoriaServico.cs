using System.Collections.Generic;
using System.Threading.Tasks;
using api_sge.Modelos;
using api_sge.Servicos;

namespace api_sge.Interfaces
{
    public interface IMercadoriaServico
    {
        Task<Resposta<List<MercadoriaObterDto>>> ListarMercadorias();

        Task<Resposta<MercadoriaObterDto>> ObterMercadoriaPorCodigo(long id);

        Task<Resposta<List<MercadoriaObterDto>>> InserirMercadoria(MercadoriaInserirDto mercadoriaNova);
    
        Task<Resposta<MercadoriaObterDto>> AtualizarMercadoria(MercadoriaAtualizarDto mercadoriaAtualizada);
        
        Task<Resposta<List<MercadoriaObterDto>>> ExcluirMercadoria(long id);

        // Task<Resposta<ObterMercadoriaDto>> AtualizarLocalizacao(AtualizarLocalizacaoMercadoriaDto mercadoriaNovaLocalizacao);
    }
}