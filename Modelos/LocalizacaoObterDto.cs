using System;

namespace api_sge.Modelos
{
    public class LocalizacaoObterDto
    {
        public long LocalizacaoCodigo { get; set; }

        public DateTime ChegadaDataHora { get; set; }

        public long IncluidoUsuarioCodigo { get; set; }

        public DateTime IncluidoDataHora { get; set; }

        public EnderecoObterDto Endereco { get; set; }
    }
}