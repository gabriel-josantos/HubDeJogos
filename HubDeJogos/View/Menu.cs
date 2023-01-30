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

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Seja bem vindo ao nosso hub de jogos!");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("");
            Console.WriteLine("Menu Principal");
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Rankings");
            Console.WriteLine("");
            Console.ResetColor();
            Console.WriteLine(new string('-', 25));
            Console.WriteLine("1 - Ranking Jogo da Velha");
            Console.WriteLine("2 - Ranking Xadrez");
            Console.WriteLine("3 - Raking Batalha Naval");
            Console.WriteLine("4 - Sair"); ;
            Console.WriteLine(new string('-', 25));
        }

        public static void MostrarMenuDeJogos()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Jogos");
            Console.WriteLine("");
            Console.ResetColor();
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1 - Jogo da Velha: Jogador vs Jogador");
            Console.WriteLine("2 - Jogo da Velha: Jogador vs Cpu");
            Console.WriteLine("3 - Xadrez: Jogador vs Jogador");
            Console.WriteLine("4 - Batalha Naval: Jogador vs Jogador");
            Console.WriteLine("5 - Sair");
            Console.WriteLine(new string('-', 40));

        }

    }
}
