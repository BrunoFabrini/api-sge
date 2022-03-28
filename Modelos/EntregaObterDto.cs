using System;
using System.Collections.Generic;
using api_sge.Enums;

namespace api_sge.Modelos
{
    public class EntregaObterDto
    {
        public long EntregaCodigo { get; set; }

        public double Frete { get; set; }

        public EntregaStatusEnum Status { get; set; }

        public long IncluidoUsuarioCodigo { get; set; }

        public DateTime IncluidoDataHora { get; set; }

        public long AlteradoUsuarioCodigo { get; set; }

        public DateTime AlteradoDataHora { get; set; }

        public MercadoriaObterDto Mercadoria { get; set; }

        public List<LocalizacaoObterDto> Localizacoes { get; set; }
    }
}