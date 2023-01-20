using System;
using System.Data;
using System.Reflection;
using System.Text.Json;
using Hub.Model;
using Hub.View;

namespace Hub
{
    public class Program
    {
 
        public static void Main(string[] args)
        {
            Console.OutputEncoding=System.Text.Encoding.Unicode;

            HubDeJogos hub = new HubDeJogos();
            JogoDaVelha velha= new JogoDaVelha();
            Xadrez xadrez= new Xadrez();
            string fileName = "JogadoresJson.json";
            Console.WriteLine("Seja bem vindo ao nosso hub de jogos!");
            Console.WriteLine("Por favor selecione uma das seguintes opções");

            int opt;
            hub.LerArquivoJsonDeJogadores(fileName);
      

            do
            {

                Menu.ShowMenu();
                opt = int.Parse(Console.ReadLine());

                switch (opt)
                {
                    case 0:
                        Console.WriteLine("Encerrando aplicação...");
                        break;
                    case 1:
                        hub.CadastrarNovoJogador(fileName);
                        break;
                    case 2:
                        hub.MostrarJogadoresCadastrados();
                        break;
                    case 3:
                        hub.AtualizarDadosDeJogador(fileName);
                        break;
                    case 4:
                        hub.DeletarJogador(fileName);
                        break;
                    case 5:                    
                        Menu.MostrarOpçoesDeRanking(velha,xadrez);
                        break;
                    case 6:
                        Menu.MostrarOpçoesDeJogos(velha,xadrez,fileName);
                        break;
                    case 7:
                        velha.ResetarPontuações(fileName);
                        break;
                    default:
                        Console.WriteLine("Opção invalida, por favor digite um valor valido");
                        break;
                }

            } while (opt != 0);

        }

    }
}
