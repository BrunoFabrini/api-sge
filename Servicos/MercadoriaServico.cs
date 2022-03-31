using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api_sge.Database;
using api_sge.Entidades;
using api_sge.Interfaces;
using api_sge.Modelos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace api_sge.Servicos
{
    public class MercadoriaServico : IMercadoriaServico
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public MercadoriaServico(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int ObterUsuarioCodigo() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<Resposta<MercadoriaObterDto>> AtualizarMercadoria(MercadoriaAtualizarDto mercadoriaAtualizada)
        {
            var resposta = new Resposta<MercadoriaObterDto>();

            try
            {
                Mercadoria mercadoria = await _context.Mercadorias.FirstOrDefaultAsync(c => c.MercadoriaCodigo == mercadoriaAtualizada.MercadoriaCodigo);                     
                if (mercadoria == null)
                {
                    resposta.Mensagem = "Mercadoria não encontrada!";
                    return resposta;
                }

                mercadoria.Descricao = mercadoriaAtualizada.Descricao;
                mercadoria.Categoria = mercadoriaAtualizada.Categoria;
                mercadoria.Valor = mercadoriaAtualizada.Valor;
                mercadoria.QuantidadeEstoque = mercadoriaAtualizada.QuantidadeEstoque;
                mercadoria.AlteradoDataHora = DateTime.Now;
                mercadoria.AlteradoUsuarioCodigo = (await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioCodigo == ObterUsuarioCodigo())).UsuarioCodigo;

                await _context.SaveChangesAsync();

                resposta.Dados = _mapper.Map<MercadoriaObterDto>(mercadoria);
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }
            
            return resposta;
        }

        public async Task<Resposta<List<MercadoriaObterDto>>> ExcluirMercadoria(long mercadoriaCodigo)
        {
            var resposta = new Resposta<List<MercadoriaObterDto>>();

            try
            {
                Mercadoria mercadoria = await _context.Mercadorias.Include(m => m.Entregas)
                                                                  .ThenInclude(e => e.Localizacoes)
                                                                  .FirstOrDefaultAsync(c => c.MercadoriaCodigo == mercadoriaCodigo); 
                if (mercadoria == null)
                {
                    resposta.Mensagem = "Mercadoria não encontrada!";
                    return resposta;
                }

                List<Entrega> entregas = mercadoria.Entregas;
                foreach (List<Localizacao> localizacoes in entregas.Select(e => e.Localizacoes).ToList())
                {
                    _context.RemoveRange(localizacoes);
                }

                _context.Entregas.RemoveRange(entregas);
                _context.Mercadorias.Remove(mercadoria);
                await _context.SaveChangesAsync();
                    
                resposta.Dados = _context.Mercadorias.Select(c => _mapper.Map<MercadoriaObterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }
            
            return resposta;
        }

        public async Task<Resposta<List<MercadoriaObterDto>>> InserirMercadoria(MercadoriaInserirDto mercadoriaNova)
        {
            var resposta = new Resposta<List<MercadoriaObterDto>>();
            
            try
            {
                Mercadoria mercadoria = _mapper.Map<Mercadoria>(mercadoriaNova);
                mercadoria.IncluidoDataHora = DateTime.Now;
                mercadoria.IncluidoUsuarioCodigo = (await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioCodigo == ObterUsuarioCodigo())).UsuarioCodigo;

                _context.Mercadorias.Add(mercadoria);

                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Mercadorias.Select(c => _mapper.Map<MercadoriaObterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }

            return resposta;
        }

        public async Task<Resposta<List<MercadoriaObterDto>>> ListarMercadorias()
        {
            var resposta = new Resposta<List<MercadoriaObterDto>>();
            
            try
            {
                resposta.Dados = await _context.Mercadorias.Select(c => _mapper.Map<MercadoriaObterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }

            return resposta;
        }

        public async Task<Resposta<MercadoriaObterDto>> ObterMercadoriaPorCodigo(long mercadoriaCodigo)
        {
            var resposta = new Resposta<MercadoriaObterDto>();
            
            try
            {
                var mercadoria = await _context.Mercadorias.FirstOrDefaultAsync(c => c.MercadoriaCodigo == mercadoriaCodigo);

                resposta.Dados = _mapper.Map<MercadoriaObterDto>(mercadoria);
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }

            return resposta;
        }
    }
}