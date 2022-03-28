using System;
using System.ComponentModel.DataAnnotations;

namespace api_sge.Entidades
{
    public class Localizacao
    {
        [Key]
        public long LocalizacaoCodigo { get; set; }

        public DateTime ChegadaDataHora { get; set; }

        public long IncluidoUsuarioCodigo { get; set; } = 0;

        public DateTime IncluidoDataHora { get; set; } = DateTime.Now;

        public Endereco Endereco { get; set; }
    }
}