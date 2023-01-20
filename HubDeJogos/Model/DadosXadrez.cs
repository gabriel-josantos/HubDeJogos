namespace Hub.Model
{
    public class DadosXadrez
    {
        public int VitoriasXadrez { get; set; }
        public int EmpatesXadrez { get; set; }
        public int DerrotasXadrez { get; set; }
        public int PontuacaoXadrez { get; set; }

        public void ObterPontuacaoXadrez(int vitorias, int empates, int derrotas)
        {

            PontuacaoXadrez = vitorias * 2 + empates * 1 - derrotas * 1;
        }

    }
}