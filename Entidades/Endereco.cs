using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_sge.Entidades
{
    public class Endereco
    {
        [Key]
        public long EnderecoCodigo { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Logradouro { get; set; }

        public string LogradouroNumero { get; set; }

        public List<Localizacao> Localizacoes { get; set; }
    }
}