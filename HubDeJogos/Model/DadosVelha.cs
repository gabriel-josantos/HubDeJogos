namespace Hub.Model
{
    public class DadosVelha
    {
        public int VitoriasVelha { get; set; }
        public int EmpatesVelha { get; set; }
        public int DerrotasVelha { get; set; }
        public int PontuacaoVelha { get; set; }

        public void ObterPontuacaoVelha(int vitorias, int empates, int derrotas)
        {

            PontuacaoVelha = vitorias * 2 + empates * 1 - derrotas * 1;
        }
    }
}