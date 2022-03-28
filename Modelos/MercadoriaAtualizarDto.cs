namespace api_sge.Modelos
{
    public class MercadoriaAtualizarDto
    {
        public long MercadoriaCodigo { get; set; }

        public string Descricao { get; set; }

        public string Categoria { get; set; }

        public double Valor { get; set; }

        public int QuantidadeEstoque { get; set; }
    }
}