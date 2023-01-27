namespace Hub.Model.xadrez
{
    public class DadosXadrez
    {
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int Pontuacao { get; set; }

        public void ObterPontuacao(int vitorias, int empates, int derrotas)
        {

            Pontuacao = vitorias * 2 + empates * 1 - derrotas * 1;
        }

    }
}