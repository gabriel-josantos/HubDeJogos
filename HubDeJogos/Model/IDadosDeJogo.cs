namespace Hub.Model
{
    public interface IDadosDeJogo
    {
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int Pontuacao { get; set; }
        public void ObterPontuacao(int vitorias, int empates, int derrotas);
        
    }
}