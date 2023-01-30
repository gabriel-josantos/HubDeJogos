

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Hub.Utils;
using Hub.Model;
using Hub.Service;

namespace Hub.Service
{
    public class JogoDaVelha : GameHub
    {

        public JogoDaVelha()
        {

        }

        public void MostrarJogoDaVelha(string[,] jogo)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            Console.WriteLine("-------------");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (j == 2)
                    {
                        Console.Write($"| ");
                        if (jogo[i, j] == "X")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{jogo[i, j]}");
                            Console.ResetColor();
                        }
                        else if (jogo[i, j] == "O")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write($"{jogo[i, j]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($" ");
                        }
                        Console.Write($" |");

                    }
                    else
                    {
                        Console.Write($"| ");
                        if (jogo[i, j] == "X")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{jogo[i, j]}");
                            Console.ResetColor();
                        }
                        else if (jogo[i, j] == "O")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write($"{jogo[i, j]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($" ");
                        }
                        Console.Write($" ");
                    }

                }
                Console.Write("\n");
            }
            Console.WriteLine("-------------");



        }

        public int ChecarCondiçãoDeVitoria(string player1, string player2, string[,] jogo, int numeroDaJogada)
        {
            int vencedor = 0;
            string[] linhas = new string[3];
            string[] colunas = new string[3];
            string diagonal = "";
            string diagonalSecundaria = "";

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    linhas[i] += jogo[i, j];
                    colunas[i] += jogo[j, i];
                    diagonal += j == i ? jogo[i, j] : "";
                    diagonalSecundaria += j == 2 - i ? jogo[i, j] : "";
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (linhas[i] == "XXX" || colunas[i] == "XXX" || diagonal == "XXX" || diagonalSecundaria == "XXX")
                {
                    vencedor = 1;
                    string message;
                    if (diagonal == "XXX" || diagonalSecundaria == "XXX")
                    {
                        message = "diagonal";
                    }
                    else
                    {
                        message = linhas[i] == "XXX" ? "linha" : "coluna";
                    }

                    MostrarJogoDaVelha(jogo);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Jogador {player1} venceu completando uma {message}");
                    return vencedor;

                }
                else if (linhas[i] == "OOO" || colunas[i] == "OOO" || diagonal == "OOO" || diagonalSecundaria == "OOO")
                {
                    vencedor = 2;
                    string message;
                    if (diagonal == "OOO" || diagonalSecundaria == "OOO")
                    {
                        message = "diagonal";
                    }
                    else
                    {
                        message = linhas[i] == "OOO" ? "linha" : "coluna";
                    }

                    MostrarJogoDaVelha(jogo);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Jogador {player2} venceu completando uma {message}");
                    return vencedor;

                }


            }

            if (numeroDaJogada == 9 && vencedor == 0)
            {
                vencedor = 3;
                MostrarJogoDaVelha(jogo);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Empate");
                return vencedor;
            }

            return 0;

        }

        public int TestarIminencias(string[] linhas, string[] colunas, string diagonal, string diagonalSecundaria, string[,] jogo, string simbolo)
        {
            for (int i = 0; i < 3; i++)
            {

                if (linhas[i] == simbolo)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (jogo[i, j] == " ")
                        {
                            return i * 3 + j + 1;

                        }
                    }
                }
                if (colunas[i] == simbolo)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (jogo[j, i] == " ")
                        {
                            return j * 3 + i + 1;

                        }
                    }
                }
                if (diagonal == simbolo)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (jogo[i, j] == " " && i == j)
                        {
                            return i * 3 + j + 1;

                        }
                    }
                }
                if (diagonalSecundaria == simbolo)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (jogo[i, j] == " " && j == 2 - i)
                        {
                            return i * 3 + j + 1;

                        }
                    }
                }
            }
            return 0;
        }
        public int ChecarIminenciaDeVitoria(string[,] jogo)
        {
            string[] linhas = new string[3];
            string[] colunas = new string[3];
            string diagonal = "";
            string diagonalSecundaria = "";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (jogo[i, j] != " ")
                    {
                        linhas[i] += jogo[i, j];
                        diagonal += j == i ? jogo[i, j] : "";
                        diagonalSecundaria += j == 2 - i ? jogo[i, j] : "";
                    }
                    //Erro foda de achar viu
                    if (jogo[j, i] != " ")
                    {
                        colunas[i] += jogo[j, i];
                    }
                }
            }
            int jogadaDeIminencia = TestarIminencias(linhas, colunas, diagonal, diagonalSecundaria, jogo, "OO");

            if (jogadaDeIminencia == 0)
            {
                jogadaDeIminencia = TestarIminencias(linhas, colunas, diagonal, diagonalSecundaria, jogo, "XX");
            }
            return jogadaDeIminencia;
        }
        public void InicializarJogo(string fileName, Jogador[] jogadores, bool cpu)
        {
            string player1;
            string player2;
            if (cpu)
            {
                player1 = "Cpu";
                player2 = Jogadores[1].Usuario;

            }
            else
            {
                player1 = jogadores[0].Usuario;
                player2 = jogadores[1].Usuario;
            }

            string[,] jogo = new string[3, 3];
            do
            {

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        jogo[i, j] = " ";
                    }
                }

                int vencedor = JogarVelha(player1, player2, jogo, cpu);
                if (!cpu)
                {
                    AtribuirResultadoDeJogo(jogadores[0], jogadores[1], vencedor, fileName, "velha");
                }
                Console.WriteLine("Deseja jogar novamente? (1 - Sim | 2 - Não)");

            } while (int.Parse(Console.ReadLine()) != 2);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

        }
        public int JogadaDoCPU(string[,] jogo, string primeiroJogagor)
        {
            int jogada = 0;
            int jogadaDeIminencia = ChecarIminenciaDeVitoria(jogo);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (jogadaDeIminencia != 0)
                    {
                        //Se player1 ou cpu estiver prestes a ganhar, preencher a casa que falta
                        jogada = jogadaDeIminencia;
                        return jogada;

                    }
                    else if (jogo[1, 1] == " " && primeiroJogagor != "CPU")
                    {
                        //&&( jogo[0, 0] != " " || jogo[0, 2] != " " || jogo[2, 0] != " " || jogo[2, 2] != " ")
                        return 5;
                    }
                    else if (jogo[i, j] == " " && j % 2 == 0 && i % 2 == 0)
                    {
                        //jogo[0,0],jogo[0,2],jogo[2,0] e jogo[2,2]
                        return i * 3 + j + 1;
                    }
                    else if (jogo[i, j] == " ")
                    {
                        jogada = i * 3 + j + 1;
                    }
                }
            }
            return jogada;
        }

        public int JogarVelha(string player1, string player2, string[,] jogo, bool cpu)
        {
            int vencedor = 0;
            int numeroDaJogada = 0;
            int vezDoJogador = 1;
            int jogada;
            string primeiroJogador = "player1";

            if (cpu)
            {
                Random rnd = new Random();
                vezDoJogador = rnd.Next(1, 3);
                primeiroJogador = vezDoJogador == 2 ? "CPU" : "player1";
            }


            do
            {
                MostrarJogoDaVelha(jogo);
                Console.ForegroundColor = ConsoleColor.DarkGreen;


                if (cpu)
                {

                    Thread.Sleep(500);
                    Console.WriteLine($"Sua vez!, por favor digite um valor entre 1 e 9 correspondente com a casa do jogo que deseja preencher");
                    jogada = vezDoJogador == 1 ? int.Parse(Console.ReadLine()) : JogadaDoCPU(jogo, primeiroJogador);

                }
                else
                {
                    string nomeDoJogador = vezDoJogador == 1 ? player1 : player2;
                    Console.WriteLine($"Vez do jogador {nomeDoJogador}, por favor digite um valor entre 1 e 9 correspondente com a casa do jogo que deseja preencher");
                    jogada = int.Parse(Console.ReadLine());
                }


                bool validInput = false;
                do
                {
                    int posição = 0;
                    while (jogada > 9 || jogada < 1)
                    {
                        Console.WriteLine("Posição invalida, por favor digite uma posição valida");
                        jogada = int.Parse(Console.ReadLine());
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {


                            if (posição == jogada - 1 && jogo[i, j] == " ")
                            {
                                numeroDaJogada++;
                                jogo[i, j] = vezDoJogador == 1 ? "X" : "O";
                                vencedor = ChecarCondiçãoDeVitoria(player1, player2, jogo, numeroDaJogada);
                                vezDoJogador = Helpers.TrocarJogador(vezDoJogador);
                                validInput= true;

                            }

                            else if (posição == jogada - 1 && jogo[i, j] != " ")
                            {
                                Console.WriteLine("Posição invalida, por favor digite uma posição valida");
                                jogada = int.Parse(Console.ReadLine());

                            }
                            posição++;

                        }

                    }
                } while (!validInput);



            } while (vencedor == 0);


            return vencedor;
        }



    }
}
