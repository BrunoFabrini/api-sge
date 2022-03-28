using api_sge.Entidades;
using api_sge.Modelos;
using AutoMapper;

namespace api_sge
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Mercadoria, MercadoriaObterDto>();
            CreateMap<MercadoriaInserirDto, Mercadoria>();
            CreateMap<Localizacao, LocalizacaoObterDto>();
            CreateMap<Entrega, EntregaObterDto>();
            CreateMap<EntregaInserirDto, Entrega>();
            CreateMap<Endereco, EnderecoObterDto>();
        }
    }
}