using System.Threading.Tasks;
using api_sge.Entidades;
using api_sge.Servicos;

namespace api_sge.Interfaces
{
    public interface IAutenticacaoServico
    {
        Task<Resposta<long>> Registrar(Usuario usuario, string senha);

        Task<Resposta<string>> Logar(string login, string senha);

        Task<bool> VerificarUsuarioExiste(string login);
    }
}