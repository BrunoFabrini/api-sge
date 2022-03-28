namespace api_sge.Servicos
{
    public class Resposta<T>
    {
        public T Dados { get; set; }

        public string Mensagem { get; set; } = string.Empty;

        public bool Sucesso
        {
            get 
            {
                return string.IsNullOrEmpty(Mensagem);
            }
        }
    }
}