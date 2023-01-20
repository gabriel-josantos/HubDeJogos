using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hub.Model
{
    public class Jogador
    {
        public string Usuario { get; set; }
        public string Nome { get; set; }
        public DadosXadrez DadosXadrez { get; set; } 

        public DadosVelha DadosVelha { get; set; }
        
 

        public Jogador(string usuario, string nome)
        {
            Usuario = usuario;
            Nome = nome;
            DadosXadrez=new DadosXadrez();
            DadosVelha= new DadosVelha(); 
        }

       
    }
}
