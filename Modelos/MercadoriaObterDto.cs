using System;
using System.Collections.Generic;

namespace api_sge.Modelos
{
    public class MercadoriaObterDto
    {
        public long MercadoriaCodigo { get; set; }

        public string Descricao { get; set; }

        public string Categoria { get; set; }

        public double Valor { get; set; }

        public int QuantidadeEstoque { get; set; }

        public long IncluidoUsuarioCodigo { get; set; }

        public DateTime IncluidoDataHora { get; set; }

        public long AlteradoUsuarioCodigo { get; set; }

        public DateTime AlteradoDataHora { get; set; }
    }
}