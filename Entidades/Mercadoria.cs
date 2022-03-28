using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_sge.Entidades
{
    public class Mercadoria
    {
        [Key]
        public long MercadoriaCodigo { get; set; }

        public string Descricao { get; set; }

        public string Categoria { get; set; }

        public double Valor { get; set; }

        public int QuantidadeEstoque { get; set; }

        public long IncluidoUsuarioCodigo { get; set; } = 0;

        public DateTime IncluidoDataHora { get; set; } = DateTime.Now;

        public long AlteradoUsuarioCodigo { get; set; } = 0;

        public DateTime AlteradoDataHora { get; set; } = DateTime.Now;

        public List<Entrega> Entregas { get; set; }
    }
}