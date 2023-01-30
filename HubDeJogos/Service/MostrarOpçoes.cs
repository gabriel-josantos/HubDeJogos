using Hub.Model.jogoDaVelha;
using Hub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hub.View;
using Hub.Service;

namespace Hub.Service
{
    public class MostrarOpçoes
    {
        public static void MostrarOpçoesDeRanking(GameHub hub)
        {
            Console.Clear();
            int opt;
            do
            {
               Menu.MostrarMenuDeRankings();
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
        public static void MostrarOpçoesDeJogos(JogoDaVelha velha, Xadrez xadrez, BatalhaNaval naval, Jogador[] jogadores, string fileName)
        {
            Console.Clear();
            int opt;
            do
            {
                Menu.MostrarMenuDeJogos();
                opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        velha.InicializarJogo(fileName, jogadores, false);
                        break;
                    case 2:
                        velha.InicializarJogo(fileName, jogadores, true);
                        break;
                    case 3:
                        xadrez.InicializarJogo(fileName, jogadores);
                        break;
                    case 4:
                        naval.InicializarJogo(fileName, jogadores);
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
