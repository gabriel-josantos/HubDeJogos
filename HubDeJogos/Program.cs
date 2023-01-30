using System;
using System.Data;
using System.Reflection;
using System.Text.Json;
using Hub.Model.jogoDaVelha;
using Hub.Model;
using Hub.View;
using Hub.Utils;
using Hub.Service;

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

            string filePath = @"..\..\..\..\data\Jogadores.Json";
            Helpers.DeserializarJson(filePath);

            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Seja bem vindo ao nosso hub de jogos!");
            Console.WriteLine(new string('-', 40));
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("Por favor faça o login dos jogadores para começar");
            Console.WriteLine("");

            Jogador[] jogadores = hub.FazerLoginDeJogadores();

            int opt;

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
                        hub.CadastrarNovoJogador(filePath);
                        break;
                    case 2:
                        hub.MostrarJogadoresCadastrados();
                        break;
                    case 3:
                        hub.AtualizarDadosDeJogador(filePath);
                        break;
                    case 4:
                        hub.DeletarJogador(filePath);
                        break;
                    case 5:
                        MostrarOpçoes.MostrarOpçoesDeRanking(hub);
                        break;
                    case 6:
                        MostrarOpçoes.MostrarOpçoesDeJogos(velha, xadrez, naval, jogadores, filePath);
                        break;
                    case 7:
                        hub.ResetarPontuações(filePath);
                        break;
                    default:
                        Console.WriteLine("Opção invalida, por favor digite um valor valido");
                        break;
                }

            } while (opt != 0);

        }

    }
}
