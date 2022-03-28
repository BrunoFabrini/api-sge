using System.ComponentModel.DataAnnotations;

namespace api_sge.Entidades
{
    public class Usuario
    {
        [Key]
        public long UsuarioCodigo { get; set; }

        public string Login { get; set; }

        public bool Admin { get; set; } = false;

        public byte[] SenhaHash { get; set; }

         public byte[] SenhaSalt { get; set; }
    }
}