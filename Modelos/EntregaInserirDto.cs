using api_sge.Enums;

namespace api_sge.Modelos
{
    public class EntregaInserirDto
    {
        public double Frete { get; set; }

        public EntregaStatusEnum Status { get; set; }

        public long MercadoriaCodigo { get; set; }
    }
}