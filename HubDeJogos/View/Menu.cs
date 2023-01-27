using Hub.Model.batalhaNaval;
using Hub.Model.jogoDaVelha;
using Hub.Model.xadrez;
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

            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Seja bem vindo ao nosso hub de jogos!");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("");
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
            Console.WriteLine("1 - Ranking Jogo da Velha");
            Console.WriteLine("2 - Ranking Xadrez");
            Console.WriteLine("3 - Raking Batalha Naval");
            Console.WriteLine("4 - Sair"); ;
            Console.WriteLine(new string('-', 21));
        }

        public static void MostrarMenuDeJogos()
        {
            Console.WriteLine(new string('-', 21));
            Console.WriteLine("1 - Jogo da Velha: Jogador vs Jogador");
            Console.WriteLine("2 - Jogo da Velha: Jogador vs Cpu");
            Console.WriteLine("3 - Xadrez: Jogador vs Jogador");
            Console.WriteLine("4 - Batalha Naval: Jogador vs Jogador");
            Console.WriteLine("5 - Sair");
            Console.WriteLine(new string('-', 21));

        }

        public static void MostrarOpçoesDeRanking(GameHub hub)
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
                        hub.MostrarRankingDeJogo("velha");
                        break;
                    case 2:
                        hub.MostrarRankingDeJogo("xadrez");       
                        break;
                    case 3:
                        hub.MostrarRankingDeJogo("naval");
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opçao invalida, por favor escolha um valor entre 1,2,3 ou 4");
                        break;
                }
            } while (opt != 4);

        }
        public static void MostrarOpçoesDeJogos(JogoDaVelha velha, Xadrez xadrez,BatalhaNaval naval,Jogador[] jogadores, string fileName)
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
                        velha.InicializarJogo(fileName,jogadores, false);
                        break;
                    case 2:
                        velha.InicializarJogo(fileName,jogadores, true);
                        break;
                    case 3:
                        xadrez.InicializarJogo(fileName,jogadores);
                        break;
                    case 4:
                        naval.InicializarJogo(fileName,jogadores);
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opçao invalida, por favor escolha um valor entre 1,2,3,4 ou 5");
                        break;
                }
            } while (opt != 5);
        }

    }
}
