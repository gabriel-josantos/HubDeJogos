namespace Hub.Model.batalhaNaval
{
    public class Navio
    {
        public int Integridade { get; set; }
        public string Tipo { get; set; }

        public string Abreviatura { get; set; }

        public Navio(int integridade, string tipo, string abreviatura)
        {
            Tipo = tipo;
            Integridade = integridade;
            Abreviatura = abreviatura;

        }
    }
}