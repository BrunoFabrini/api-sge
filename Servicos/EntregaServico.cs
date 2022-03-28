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
    public class EntregaServico : IEntregaServico
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public EntregaServico(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int ObterUsuarioCodigo() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<Resposta<EntregaObterDto>> AtualizarEntrega(EntregaAtualizarDto entregaAtualizada)
        {
            var resposta = new Resposta<EntregaObterDto>();

            try
            {
                Entrega entrega = await _context.Entregas.FirstOrDefaultAsync(c => c.EntregaCodigo == entregaAtualizada.EntregaCodigo);                     
                if (entrega == null)
                {
                    resposta.Mensagem = "Entrega não encontrada!";
                    return resposta;
                }

                entrega.Status = entregaAtualizada.Status;
                entrega.AlteradoDataHora = DateTime.Now;
                entrega.AlteradoUsuarioCodigo = (await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioCodigo == ObterUsuarioCodigo())).UsuarioCodigo;

                await _context.SaveChangesAsync();

                resposta.Dados = _mapper.Map<EntregaObterDto>(entrega);
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }
            
            return resposta;
        }

        public async Task<Resposta<EntregaObterDto>> AtualizarLocalizacao(EntregaAtualizarLocalizacaoDto novaEntregaLocalizacao)
        {
            var resposta = new Resposta<EntregaObterDto>();

            try
            {
                Entrega entrega = await _context.Entregas.FirstOrDefaultAsync(c => c.EntregaCodigo == novaEntregaLocalizacao.EntregaCodigo);  
                if (entrega == null)
                {
                    resposta.Mensagem = "Entrega não encontrada!";
                    return resposta;
                }

                entrega.AlteradoDataHora = DateTime.Now;
                entrega.AlteradoUsuarioCodigo = (await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioCodigo == ObterUsuarioCodigo())).UsuarioCodigo;
                
                Endereco endereco = await _context.Enderecos.FirstAsync(s => s.EnderecoCodigo == novaEntregaLocalizacao.EnderecoCodigo);
                if (endereco == null)
                {
                    resposta.Mensagem = "Endereço não encontrado!";
                    return resposta;
                }

                Localizacao localizacao = new Localizacao();
                localizacao.Endereco = endereco;
                localizacao.ChegadaDataHora = DateTime.Now;

                //Atualiza a localização da entrega de forma transacional
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Localizacoes.Add(localizacao);
                    await _context.SaveChangesAsync();

                    if (entrega.Localizacoes == null)
                    {
                        entrega.Localizacoes = new List<Localizacao>();
                    }

                    entrega.Localizacoes.Add(localizacao);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                
                entrega = await _context.Entregas.Include(e => e.Mercadoria)
                                                 .Include(e => e.Localizacoes)
                                                 .ThenInclude(l => l.Endereco)
                                                 .FirstOrDefaultAsync(c => c.EntregaCodigo == novaEntregaLocalizacao.EntregaCodigo); 
                if (entrega == null)
                {
                    resposta.Mensagem = "Entrega não encontrada!";
                    return resposta;
                }

                resposta.Dados = _mapper.Map<EntregaObterDto>(entrega);
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }
            return resposta;
        }

        public async Task<Resposta<List<EntregaObterDto>>> ExcluirEntrega(long EntregaCodigo)
        {
            var resposta = new Resposta<List<EntregaObterDto>>();

            try
            {
                Entrega entrega = await _context.Entregas.FirstOrDefaultAsync(c => c.EntregaCodigo == EntregaCodigo); 
                if (entrega == null)
                {
                    resposta.Mensagem = "Entrega não encontrada!";
                    return resposta;
                }

                _context.Entregas.Remove(entrega);
                await _context.SaveChangesAsync();
                    
                resposta.Dados = _context.Entregas.Select(c => _mapper.Map<EntregaObterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }
            
            return resposta;
        }

        public async Task<Resposta<List<EntregaObterDto>>> InserirEntrega(EntregaInserirDto entregaNova)
        {
            var resposta = new Resposta<List<EntregaObterDto>>();
            
            try
            {
                Mercadoria mercadoria =  await _context.Mercadorias.FirstOrDefaultAsync(m => m.MercadoriaCodigo == entregaNova.MercadoriaCodigo);
                if (mercadoria == null)
                {
                    resposta.Mensagem = "Mercadoria não encontrada!";
                    return resposta;
                }

                Entrega entrega = _mapper.Map<Entrega>(entregaNova);
                entrega.Mercadoria = mercadoria;
                entrega.IncluidoDataHora = DateTime.Now;
                entrega.IncluidoUsuarioCodigo = (await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioCodigo == ObterUsuarioCodigo())).UsuarioCodigo;

                _context.Entregas.Add(entrega);

                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Entregas.Include(e => e.Mercadoria)
                                                        .Include(e => e.Localizacoes)
                                                        .ThenInclude(l => l.Endereco)
                                                        .Select(c => _mapper.Map<EntregaObterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }

            return resposta;
        }

        public async Task<Resposta<List<EntregaObterDto>>> ListarEntregas()
        {
            var resposta = new Resposta<List<EntregaObterDto>>();
            
            try
            {
                resposta.Dados = await _context.Entregas.Include(e => e.Mercadoria)
                                                        .Include(e => e.Localizacoes)
                                                        .ThenInclude(l => l.Endereco)
                                                        .Select(c => _mapper.Map<EntregaObterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }

            return resposta;
        }

        public async Task<Resposta<EntregaObterDto>> ObterEntregaPorCodigo(long EntregaCodigo)
        {
            var resposta = new Resposta<EntregaObterDto>();
            
            try
            {
                var entrega = await _context.Entregas.Include(e => e.Mercadoria)
                                                     .Include(e => e.Localizacoes)
                                                     .ThenInclude(l => l.Endereco)
                                                     .FirstOrDefaultAsync(c => c.EntregaCodigo == EntregaCodigo);

                resposta.Dados = _mapper.Map<EntregaObterDto>(entrega);
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
            }

            return resposta;
        }
    }
}