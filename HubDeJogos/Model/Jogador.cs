using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Hub.Model.batalhaNaval;
using Hub.Model.jogoDaVelha;
using Hub.Model.xadrez;

namespace Hub.Model
{
    public class Jogador
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public DadosXadrez DadosXadrez { get; set; }

        public DadosVelha DadosVelha { get; set; }

        public DadosNaval DadosNaval { get; set; }

        public Jogador(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
            DadosXadrez = new DadosXadrez();
            DadosVelha = new DadosVelha();
            DadosNaval= new DadosNaval();
        }


    }
}
