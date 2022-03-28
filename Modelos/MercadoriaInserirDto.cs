namespace api_sge.Modelos
{
    public class MercadoriaInserirDto
    {
        public string Descricao { get; set; }

        public string Categoria { get; set; }

        public double Valor { get; set; }

        public int QuantidadeEstoque { get; set; }
    }
}