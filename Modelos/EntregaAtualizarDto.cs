using api_sge.Enums;

namespace api_sge.Modelos
{
    public class EntregaAtualizarDto
    {
        public long EntregaCodigo { get; set; }

        public EntregaStatusEnum Status { get; set; }
    }
}