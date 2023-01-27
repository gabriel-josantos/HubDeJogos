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
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            GameHub hub = new GameHub();
            JogoDaVelha velha = new JogoDaVelha();
            Xadrez xadrez = new Xadrez();
            BatalhaNaval naval = new BatalhaNaval();
            string fileName = "JogadoresJson.json";
            hub.LerArquivoJsonDeJogadores(fileName);

            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Seja bem vindo ao nosso hub de jogos!");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("");
            Console.WriteLine("Por favor faça o login dos jogadores para começar");

            Jogador[] jogadores = hub.FazerLoginDeJogadores();

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
                        Menu.MostrarOpçoesDeRanking(hub);
                        break;
                    case 6:
                        Menu.MostrarOpçoesDeJogos(velha, xadrez, naval, jogadores, fileName);
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
