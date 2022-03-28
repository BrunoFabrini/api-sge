using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api_sge.Enums;

namespace api_sge.Entidades
{
    public class Entrega
    {
        [Key]
        public long EntregaCodigo { get; set; }

        public double Frete { get; set; }

        public EntregaStatusEnum Status { get; set; }

        public long IncluidoUsuarioCodigo { get; set; } = 0;

        public DateTime IncluidoDataHora { get; set; } = DateTime.Now;

        public long AlteradoUsuarioCodigo { get; set; } = 0;

        public DateTime AlteradoDataHora { get; set; } = DateTime.Now;

        public Mercadoria Mercadoria { get; set; }

        public List<Localizacao> Localizacoes { get; set; }
    }
}