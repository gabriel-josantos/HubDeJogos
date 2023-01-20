using Hub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.View
{
    public class Menu
    {
        public static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - ");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("1 - Cadastrar novo jogador");
            Console.WriteLine("2 - Mostrar jogadores");
            Console.WriteLine("3 - Atualizar informações de jogador");
            Console.WriteLine("4 - Deletar jogador");
            Console.WriteLine("5 - Mostrar rankings");
            Console.WriteLine("6 - Jogar jogos");
            Console.WriteLine("7 - Resetar pontuações");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - ");
        }

        public static void MostrarMenuDeRankings()
        {
            Console.WriteLine(new string('-', 21));
            Console.WriteLine("1 - Ranking jogo da velha");
            Console.WriteLine("2 - Ranking xadrez");
            Console.WriteLine("3 - Ranking terceiro jogo");
            Console.WriteLine("4 - Sair"); ;
            Console.WriteLine(new string('-', 21));
        }

        public static void MostrarMenuDeJogos()
        {
            Console.WriteLine(new string('-', 21));
            Console.WriteLine("1 - Jogo da Velha: Jogador vs Jogador");
            Console.WriteLine("2 - Jogo da Velha: Jogador vs Cpu");
            Console.WriteLine("3 - Xadrez: Jogador vs Jogador");
            Console.WriteLine("4 - Terceiro jogo");
            Console.WriteLine("5 - Sair");
            Console.WriteLine(new string('-', 21));

        }

        public static void MostrarOpçoesDeRanking(JogoDaVelha velha, Xadrez xadrez)
        {
            Console.Clear();
            int opt;
            do
            {
                MostrarMenuDeRankings();
                opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        velha.MostrarRankingJogoDaVelha();
                        break;
                    case 2:
                        xadrez.MostrarRankingXadrez();
                        break;
                    case 3:
                        Console.WriteLine("Mostrar raking terceiro jogo");
                        break;
                    case 4:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opçao invalida, por favor escolha um valor entre 1,2,3 ou 4");
                        break;
                }
            } while (opt != 4);

        }
        public static void MostrarOpçoesDeJogos(JogoDaVelha velha, Xadrez xadrez, string fileName)
        {
            Console.Clear();
            int opt;
            do
            {
                MostrarMenuDeJogos();
                opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        velha.InicializarJogo(fileName, false);
                        break;
                    case 2:
                        velha.InicializarJogo(fileName, true);
                        break;
                    case 3:
                        xadrez.InicializarJogo(fileName);
                        break;
                    case 4:
                        Console.WriteLine("Jogar terceiro jogo");
                        break;
                    case 5:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opçao invalida, por favor escolha um valor entre 1,2,3,4 ou 5");
                        break;
                }
            } while (opt != 5);
        }

    }
}
