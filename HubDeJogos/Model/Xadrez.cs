using Hub.Model.Peças;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hub.Model
{
    public class Xadrez : HubDeJogos
    {
        /*
         Ler o tabueleiro,e pegar as posições atuais das peças*/
        public Xadrez() { }

        private Peça _peça { get; set; } = new Peça();

        private string _peaoPreto = "\x265F";
        private string _peaoBranco = "\x2659";
        private string[] _peçasPretas = { "\x265C", "\x265E", "\x265D", "\x265A", "\x265B", "\x265D", "\x265E", "\x265C" };
        private string[] _peçasBrancas = { "\x2656", "\x2658", "\x2657", "\x2654", "\x2655", "\x2657", "\x2658", "\x2656" };
        private int[] posiçãoPeçasPretas = { 1, 2, 3, 4, 5, 6, 7, 8 };//{Torre,Cavalo,Bispo,Rainha,Rei,Bispo,Cavalo,Torre}
        private int[] posiçãoPeçasBrancas = { 57, 58, 59, 60, 61, 62, 63, 64 };//{Torre,Cavalo,Bispo,Rei,Rainha,Bispo,Cavalo,Torre}

        public int[] RetornarPosiçãoNoTabuleiro(int posiçãoAbsoluta)
        {
            int linha = (posiçãoAbsoluta - 1) / 8;
            int coluna = posiçãoAbsoluta - linha * 8 - 1;

            int[] coords = { linha, coluna };

            return coords;
        }

        public int GetIndexArray(int[] arr, int value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public int[] ValidarInputDeDados()
        {
            string casaStr = Console.ReadLine();
            int[] casa = TranformarStringParaCoords(casaStr);

            while (casa[1] == -1 || casa[0] < 0 || casa[0] > 7)
            {
                Console.WriteLine("Casa não valida, digite uma casa valida");
                casaStr = Console.ReadLine();
                casa = TranformarStringParaCoords(casaStr);
            }
            return casa;
        }

        public int[] TranformarStringParaCoords(string casaInicialStr)
        {
            string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h" };
            int linha = int.Parse(casaInicialStr[1].ToString());
            int coluna = -1;
            string colunaChar = casaInicialStr[0].ToString();

            for (int i = 0; i < letras.Length; i++)
            {
                if (colunaChar == letras[i])
                {
                    coluna = i;

                }
            }
            int[] coords = { linha, coluna };
            return coords;



        }

        public void MostrarRankingXadrez()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            int[] pontosDeJogadores = new int[Jogadores.Count];
            List<string> usuariosCopia = new List<string>();
            for (int i = 0; i < Jogadores.Count; i++)
            {
                pontosDeJogadores[i] = Jogadores[i].DadosXadrez.PontuacaoXadrez;
                usuariosCopia.Add(Jogadores[i].Usuario);
            }
            Array.Sort(pontosDeJogadores);
            Array.Reverse(pontosDeJogadores);
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("| Jogador | Pontos | Vitorias | Derrotas | Empates |");

            for (int j = 0; j < Jogadores.Count; j++)
            {
                //achar o index do player correspondete a primeira pontuação no vetor pontos
                //Dificil
                Jogador jogador = Jogadores.Find(player => player.DadosXadrez.PontuacaoXadrez == pontosDeJogadores[j] && usuariosCopia.Exists(jogador => player.Usuario == jogador));
                Console.WriteLine("|{0,9}|{1,8}|{2,10}|{3,10}|{4,9}|", jogador.Usuario, pontosDeJogadores[j], jogador.DadosXadrez.VitoriasXadrez, jogador.DadosXadrez.DerrotasXadrez, jogador.DadosXadrez.EmpatesXadrez);
                usuariosCopia.Remove(jogador.Usuario);

            }
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("");




        }

        public string[,] CriarTabuleiro()
        {
            Console.Clear();
            string[,] tabuleiro = new string[8, 8];

            Console.WriteLine(new string('-', 27));
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{i} ");
                for (int j = 0; j < 8; j++)
                {

                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        if (i == 0)
                        {
                            tabuleiro[i, j] = _peçasPretas[j];

                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" {_peçasPretas[j]} ");
                            Console.ResetColor();

                        }
                        else if (i == 1)
                        {
                            tabuleiro[i, j] = _peaoPreto;

                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" {_peaoPreto} ");
                            Console.ResetColor();
                        }
                        else if (i == 6)
                        {
                            tabuleiro[i, j] = _peaoBranco;

                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" {_peaoBranco} ");
                            Console.ResetColor();

                        }
                        else if (i == 7)
                        {
                            tabuleiro[i, j] = _peçasBrancas[j];

                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" {_peçasBrancas[j]} ");
                            Console.ResetColor();
                        }
                        else
                        {
                            tabuleiro[i, j] = " ";

                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"| |");
                            Console.ResetColor();
                        }


                    }
                    else
                    {
                        if (i == 0)
                        {
                            tabuleiro[i, j] = _peçasPretas[j];

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" {_peçasPretas[j]} ");
                            Console.ResetColor();
                        }
                        else if (i == 1)
                        {
                            tabuleiro[i, j] = _peaoPreto;

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" {_peaoPreto} ");
                            Console.ResetColor();
                        }
                        else if (i == 6)
                        {
                            tabuleiro[i, j] = _peaoBranco;

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" {_peaoBranco} ");
                            Console.ResetColor();
                        }
                        else if (i == 7)
                        {
                            tabuleiro[i, j] = _peçasBrancas[j];

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" {_peçasBrancas[j]} ");
                            Console.ResetColor();
                        }
                        else
                        {
                            tabuleiro[i, j] = " ";

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write($"| |");
                            Console.ResetColor();
                        }

                    }

                }
                Console.Write("\n");
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.WriteLine(new string('-', 27));

            Console.WriteLine("Turorial:");
            Console.WriteLine("");
            Console.WriteLine(" Para jogar, basta digitar a posição da peça que deseja movimentar,como mostradado no tabuleiro");
            Console.WriteLine(" Entao digite a posição da casa para qual deseja mover sua peça");
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
            return tabuleiro;
        }

        public string[,] MostrarTabuleiro(string[,] tabuleiro)
        {
            Console.Clear();
            Console.WriteLine(new string('-', 27));

            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{i} ");
                for (int j = 0; j < 8; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($" {tabuleiro[i, j]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($" {tabuleiro[i, j]} ");
                        Console.ResetColor();
                    }
                }
                Console.Write("\n");
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.WriteLine(new string('-', 27));
            return tabuleiro;
        }
        public bool CondiçãoDeMovimentoDoCavalo(int linhaFinal, int colunaFinal, int linhaInicial, int colunaInicial)
        {
            bool casaFinalSatisfazCavalo =
                (linhaFinal == linhaInicial + 2 && colunaFinal == colunaInicial + 1) ||
                (linhaFinal == linhaInicial - 2 && colunaFinal == colunaInicial + 1) ||
                (linhaFinal == linhaInicial + 2 && colunaFinal == colunaInicial - 1) ||
                (linhaFinal == linhaInicial - 2 && colunaFinal == colunaInicial - 1) ||
                (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial + 2) ||
                (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial - 2) ||
                (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial + 2) ||
                (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial - 2);

            return casaFinalSatisfazCavalo;
        }

        public bool CondiçãoDeMovimentoDaTorre(int linhaFinal, int colunaFinal, int linhaInicial, int colunaInicial, string[,] tabuleiro)
        {
            bool condiçãoQueSatisfazTorre = linhaFinal == linhaInicial || colunaFinal == colunaInicial;
            //Se ele se mover na vertical
            int distanciaVertical = Math.Abs(linhaFinal - linhaInicial);
            int distanciaHorizontal = Math.Abs(colunaFinal - colunaInicial);
            int distancia = Math.Abs(distanciaVertical - distanciaHorizontal);


            if (linhaFinal < linhaInicial)
            {
                for (int i = 0; i < distancia - 1; i++)
                {
                    linhaInicial--;
                    if (tabuleiro[linhaInicial, colunaInicial] != " ")
                    {
                        return false;
                    }

                }
            }
            else if (linhaFinal > linhaInicial)
            {
                for (int i = 0; i < distancia - 1; i++)
                {
                    linhaInicial++;
                    if (tabuleiro[linhaInicial, colunaInicial] != " ")
                    {
                        return false;
                    }

                }
            }
            else if (colunaFinal > colunaInicial)
            {
                for (int i = 0; i < distancia - 1; i++)
                {
                    colunaInicial--;
                    if (tabuleiro[linhaInicial, colunaInicial] != " ")
                    {
                        return false;
                    }

                }
            }
            else if (colunaFinal < colunaInicial)
            {
                for (int i = 0; i < distancia - 1; i++)
                {
                    colunaInicial++;
                    if (tabuleiro[linhaInicial, colunaInicial] != " ")
                    {
                        return false;
                    }

                }
            }


            return condiçãoQueSatisfazTorre;
        }

        public bool CondiçãoDeMovimentoDoRei(int linhaFinal, int colunaFinal, int linhaInicial, int colunaInicial)
        {
            bool condiçãoQueSatisfazRei =
                (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial + 1) ||
                (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial - 1) ||
                (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial + 1) ||
                (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial - 1) ||
                (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial) ||
                (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial) ||
                (linhaFinal == linhaInicial && colunaFinal == colunaInicial + 1) ||
                (linhaFinal == linhaInicial && colunaFinal == colunaInicial - 1);


            return condiçãoQueSatisfazRei;
        }
        public bool CondiçãoDeMovimentoDoBispo(int linhaFinal, int colunaFinal, int linhaInicial, int colunaInicial, string[,] tabuleiro)
        {
            int distanciaVertical = Math.Abs(linhaFinal - linhaInicial);
            int distanciaHorizontal = Math.Abs(colunaFinal - colunaInicial);
            bool condiçãoQueSatisfazBispo = distanciaHorizontal == distanciaVertical;

            int distancia = distanciaHorizontal; ;


            if (linhaFinal < linhaInicial && colunaFinal > colunaInicial)
            {
                for (int i = 0; i < distancia - 1; i++)
                {
                    linhaInicial--;
                    colunaInicial++;
                    if (tabuleiro[linhaInicial, colunaInicial] != " ")
                    {
                        return false;
                    }

                }
            }
            else if (linhaFinal < linhaInicial && colunaFinal < colunaInicial)
            {
                for (int i = 0; i < distancia - 1; i++)
                {
                    linhaInicial--;
                    colunaInicial--;
                    if (tabuleiro[linhaInicial, colunaInicial] != " ")
                    {
                        return false;
                    }

                }
            }
            else if (linhaFinal > linhaInicial && colunaFinal > colunaInicial)
            {
                for (int i = 0; i < distancia - 1; i++)
                {
                    linhaInicial++;
                    colunaInicial++;
                    if (tabuleiro[linhaInicial, colunaInicial] != " ")
                    {
                        return false;
                    }

                }
            }
            else if (linhaFinal > linhaInicial && colunaFinal < colunaInicial)
            {
                for (int i = 0; i < distancia - 1; i++)
                {
                    linhaInicial++;
                    colunaInicial--;
                    if (tabuleiro[linhaInicial, colunaInicial] != " ")
                    {
                        return false;
                    }

                }
            }

            return condiçãoQueSatisfazBispo;
        }

        public bool CondiçãoDeMovimentoDoPeão(int linhaFinal, int colunaFinal, int linhaInicial, int colunaInicial, string corDaPeça, int numPeça)
        {
            bool condiçãoQueSatisfazPeão;
            if (corDaPeça == "branca")
            {
                if (_peça.PeaoBranco.Movimento[numPeça] == 0)
                {
                    condiçãoQueSatisfazPeão = (linhaFinal == linhaInicial - 2 && colunaFinal == colunaInicial) || (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial);
                }
                else
                {
                    condiçãoQueSatisfazPeão = (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial);
                }
                return condiçãoQueSatisfazPeão;
            }
            else
            {
                if (_peça.PeaoPreto.Movimento[numPeça] == 0)
                {
                    condiçãoQueSatisfazPeão = (linhaFinal == linhaInicial + 2 && colunaFinal == colunaInicial) || (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial);
                }
                else
                {
                    condiçãoQueSatisfazPeão = (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial);
                }


                return condiçãoQueSatisfazPeão;
            }
        }

        public bool CondiçãoDeAtaqueDoPeão(int linhaFinal, int colunaFinal, int linhaInicial, int colunaInicial, string[,] tabuleiro, string corDaPeça)
        {
            bool condiçãoQueSatisfazAtaque;

            string valorDaCasaFinal = tabuleiro[linhaFinal, colunaFinal];

            bool casaFinalÉpeçaPreta = _peçasPretas.Contains(valorDaCasaFinal) || _peaoPreto.Equals(valorDaCasaFinal);
            bool casaFinalÉPeçaBranca = _peçasBrancas.Contains(valorDaCasaFinal) || _peaoBranco.Equals(valorDaCasaFinal);

            bool casaFinalPodeSerAtacada = corDaPeça == "branca" ? casaFinalÉpeçaPreta : casaFinalÉPeçaBranca;
            if (casaFinalPodeSerAtacada)
            {
                if (corDaPeça == "branca")
                {
                    condiçãoQueSatisfazAtaque =
                    (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial + 1) ||
                    (linhaFinal == linhaInicial - 1 && colunaFinal == colunaInicial - 1);
                    return condiçãoQueSatisfazAtaque;
                }
                else
                {
                    condiçãoQueSatisfazAtaque =
                    (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial + 1) ||
                    (linhaFinal == linhaInicial + 1 && colunaFinal == colunaInicial - 1);
                    return condiçãoQueSatisfazAtaque;
                }
            }

            return false;
        }

        public string[,] MoverPeao(string corDaPeça, int[] casaInicial, string[,] tabuleiro)
        {
            Console.WriteLine("Selecione a casa para qual deseja mover sua peça");

            int[] casaFinal = ValidarInputDeDados();
            do
            {
                int posicaoAbsolutaInicial = 8 * casaInicial[0] + casaInicial[1] + 1;
                int posicaoAbsolutaFinal = 8 * casaFinal[0] + casaFinal[1] + 1;
                int linhaInicial = casaInicial[0];
                int colunaInicial = casaInicial[1];
                bool estaDentroDoTabuleiro = linhaInicial >= 0 && linhaInicial <= 7 && colunaInicial >= 0 && colunaInicial <= 7;

                int numPeça = corDaPeça == "branca" ? GetIndexArray(_peça.PeaoBranco.Posições, posicaoAbsolutaInicial) : GetIndexArray(_peça.PeaoPreto.Posições, posicaoAbsolutaInicial);

                if (estaDentroDoTabuleiro)
                {
                    bool casaFinalEstaVazia = tabuleiro[casaFinal[0], casaFinal[1]] == " ";
                    bool casaFinalSatisfazPeão = CondiçãoDeMovimentoDoPeão(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, corDaPeça, numPeça);

                    bool peaoPodeAtacar = CondiçãoDeAtaqueDoPeão(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro, corDaPeça);

                    if ((casaFinalEstaVazia || peaoPodeAtacar) && (casaFinalSatisfazPeão || peaoPodeAtacar))
                    {
                        if (corDaPeça == "branca")
                        {
                            _peça.PeaoBranco.Posições[numPeça] = posicaoAbsolutaFinal;
                            _peça.PeaoBranco.Movimento[numPeça]++;
                        }
                        if (corDaPeça == "preta")
                        {
                            _peça.PeaoPreto.Posições[numPeça] = posicaoAbsolutaFinal;
                            _peça.PeaoPreto.Movimento[numPeça]++;
                        }

                        tabuleiro[casaFinal[0], casaFinal[1]] = tabuleiro[linhaInicial, colunaInicial];
                        tabuleiro[linhaInicial, colunaInicial] = " ";

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Posição invalida, por favor digite novamente ");
                        casaFinal = ValidarInputDeDados();
                    }

                }
                if (!estaDentroDoTabuleiro)
                {
                    Console.WriteLine("Posição invalida, por favor digite novamente ");
                    casaFinal = ValidarInputDeDados();
                }


            }
            while (true);




            return tabuleiro;
        }
        public string[,] MoverPeça(string corDaPeça, int[] casaInicial, string[,] tabuleiro, string peça)
        {

            Console.WriteLine("Selecione a casa para qual deseja mover sua peça");

            int[] casaFinal = ValidarInputDeDados();

            do
            {
                bool movimentoÉvalido = false;
                int posicaoAbsolutaInicial = casaInicial[0] * 8 + casaInicial[1] + 1;
                int posicaoAbsolutaFinal = 8 * casaFinal[0] + casaFinal[1] + 1;
                int linhaInicial = casaInicial[0];
                int colunaInicial = casaInicial[1];
                int numPeça = corDaPeça == "branca" ? GetIndexArray(posiçãoPeçasBrancas, posicaoAbsolutaInicial) : GetIndexArray(posiçãoPeçasPretas, posicaoAbsolutaInicial);


                bool estaDentroDoTabuleiro = linhaInicial >= 0 && linhaInicial <= 7 && colunaInicial >= 0 && colunaInicial <= 7;

                string valorDaCasaFinal = tabuleiro[casaFinal[0], casaFinal[1]];



                bool casaFinalÉpeçaPreta = _peçasPretas.Contains(valorDaCasaFinal) || _peaoPreto.Equals(valorDaCasaFinal);
                bool casaFinaléPeçaBranca = _peçasBrancas.Contains(valorDaCasaFinal) || _peaoBranco.Equals(valorDaCasaFinal);


                if (estaDentroDoTabuleiro)
                {
                    //E casaFinal não é inimiga
                    bool casaFinalEstaVazia = valorDaCasaFinal == " ";
                    switch (peça)
                    {
                        case "Torre": movimentoÉvalido = CondiçãoDeMovimentoDaTorre(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro); break;
                        case "Cavalo": movimentoÉvalido = CondiçãoDeMovimentoDoCavalo(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial); break;
                        case "Bispo": movimentoÉvalido = CondiçãoDeMovimentoDoBispo(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro); break;
                        case "Rei": movimentoÉvalido = CondiçãoDeMovimentoDoRei(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial); break;
                        case "Rainha": movimentoÉvalido = CondiçãoDeMovimentoDaTorre(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro) || CondiçãoDeMovimentoDoBispo(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro); break;
                    }

                    bool CasaFinalPodeSerAtacada = corDaPeça == "branca" ? casaFinalÉpeçaPreta : casaFinaléPeçaBranca;

                    if ((CasaFinalPodeSerAtacada || casaFinalEstaVazia) && movimentoÉvalido)
                    {
                        if (corDaPeça == "branca")
                        {
                            posiçãoPeçasBrancas[numPeça] = posicaoAbsolutaFinal;
                        }
                        if (corDaPeça == "preta")
                        {
                            posiçãoPeçasPretas[numPeça] = posicaoAbsolutaFinal;
                        }

                        tabuleiro[casaFinal[0], casaFinal[1]] = tabuleiro[linhaInicial, colunaInicial];
                        tabuleiro[linhaInicial, colunaInicial] = " ";

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Posição invalida, por favor digite novamente ");
                        casaFinal = ValidarInputDeDados();
                    }

                }
                if (!estaDentroDoTabuleiro)
                {
                    Console.WriteLine("Posição invalida, por favor digite novamente ");
                    casaFinal = ValidarInputDeDados();
                }


            }
            while (true);




            return tabuleiro;


        }
        public void InicializarJogo(string fileName)
        {
            //Console.WriteLine("Digite o usuario do Jogador 1");
            //string player1 = Console.ReadLine();
            //Console.WriteLine("Digite o usuario do Jogador 2");
            //string player2 = Console.ReadLine();

            if (ChecarExistenciaDeJogador("Gabriel") && ChecarExistenciaDeJogador("Arthur"))
            {


                int vencedor = JogarXadrez("Gabriel", "Arthur");

                AtribuirResultadoXadrez("Gabriel", "Arthur", vencedor, fileName);


            }
            else
            {
                Console.WriteLine("usuário inexistente");
            }
        }
        public int JogarXadrez(string player1, string player2)
        {
            int vezDoJogador = 1;
            int vencedor;
            bool opçaoValida = true;
            string corDaPeça;
            string[,] tabuleiro = CriarTabuleiro();
            do
            {
                tabuleiro = MostrarTabuleiro(tabuleiro);

                corDaPeça = vezDoJogador == 1 ? "branca" : "preta";
                string nomeDoJogador = vezDoJogador == 1 ? player1 : player2;

                vencedor = ChecarSeReiFoiCapturado(corDaPeça, tabuleiro, player1, player2);

                if (vencedor == 1 || vencedor == 2)
                {
                    return vencedor;
                }

                Console.WriteLine($"Vez do jogador {nomeDoJogador}, selecione a casa da peça que deseja mover");

                ChecarXeque(corDaPeça, tabuleiro);

                int[] casaInicial = ValidarInputDeDados();

                string peça = tabuleiro[casaInicial[0], casaInicial[1]];

                while (true)
                {

                    switch (peça)
                    {
                        case "\x2659" or "\x265F"://Peao
                            tabuleiro = MoverPeao(corDaPeça, casaInicial, tabuleiro);
                            opçaoValida = true;
                            break;
                        case "\x265C" or "\x2656"://Torre
                            tabuleiro = MoverPeça(corDaPeça, casaInicial, tabuleiro, "Torre");
                            opçaoValida = true;
                            break;
                        case "\x265E" or "\x2658"://Cavalo
                            tabuleiro = MoverPeça(corDaPeça, casaInicial, tabuleiro, "Cavalo");
                            opçaoValida = true;
                            break;
                        case "\x265D" or "\x2657"://Bispo
                            tabuleiro = MoverPeça(corDaPeça, casaInicial, tabuleiro, "Bispo");
                            opçaoValida = true;
                            break;
                        case "\x2655" or "\x265A"://Rei
                            tabuleiro = MoverPeça(corDaPeça, casaInicial, tabuleiro, "Rei");
                            break;
                        case "\x265B" or "\x2654"://Rainha
                            tabuleiro = MoverPeça(corDaPeça, casaInicial, tabuleiro, "Rainha");
                            opçaoValida = true;
                            break;
                        default:
                            Console.WriteLine("casa vazia, tente novamente: ");
                            opçaoValida = false;
                            break;
                    }
                    if (opçaoValida)
                    {
                        break;
                    }
                    else
                    {
                        peça = Console.ReadLine();
                    }
                }

                vezDoJogador = TrocarJogador(vezDoJogador);





            } while (true);



        }

        public void ChecarXeque(string corDaPeça, string[,] tabuleiro)
        {

            string corDoOponente = corDaPeça == "branca" ? "preta" : "branca";

            int posiçãoRei = corDaPeça == "branca" ? posiçãoPeçasBrancas[3] : posiçãoPeçasPretas[3];
            int[] casaRei = RetornarPosiçãoNoTabuleiro(posiçãoRei);


            int posiçãoTorre1 = corDoOponente == "branca" ? posiçãoPeçasBrancas[0] : posiçãoPeçasPretas[0];
            int[] casaTorre1 = RetornarPosiçãoNoTabuleiro(posiçãoTorre1);
            int posiçãoTorre2 = corDoOponente == "branca" ? posiçãoPeçasBrancas[7] : posiçãoPeçasPretas[7];
            int[] casaTorre2 = RetornarPosiçãoNoTabuleiro(posiçãoTorre2);
            int posiçãoCavalo1 = corDoOponente == "branca" ? posiçãoPeçasBrancas[1] : posiçãoPeçasPretas[1];
            int[] casaCavalo1 = RetornarPosiçãoNoTabuleiro(posiçãoCavalo1);
            int posiçãoCavalo2 = corDoOponente == "branca" ? posiçãoPeçasBrancas[6] : posiçãoPeçasPretas[6];
            int[] casaCavalo2 = RetornarPosiçãoNoTabuleiro(posiçãoCavalo2);
            int posiçãoBispo1 = corDoOponente == "branca" ? posiçãoPeçasBrancas[2] : posiçãoPeçasPretas[2];
            int[] casaBispo1 = RetornarPosiçãoNoTabuleiro(posiçãoBispo1);
            int posiçãoBispo2 = corDoOponente == "branca" ? posiçãoPeçasBrancas[5] : posiçãoPeçasPretas[5];
            int[] casaBispo2 = RetornarPosiçãoNoTabuleiro(posiçãoBispo2);
            int posiçãoRainha = corDoOponente == "branca" ? posiçãoPeçasBrancas[4] : posiçãoPeçasPretas[3];
            int[] casaRainha = RetornarPosiçãoNoTabuleiro(posiçãoRainha);

            bool chequeDeTorre1 = CondiçãoDeMovimentoDaTorre(casaTorre1[0], casaTorre1[1], casaRei[0], casaRei[1], tabuleiro);
            bool chequeDeTorre2 = CondiçãoDeMovimentoDaTorre(casaTorre2[0], casaTorre2[1], casaRei[0], casaRei[1], tabuleiro);
            bool chequeDeCavalo1 = CondiçãoDeMovimentoDoCavalo(casaCavalo1[0], casaCavalo1[1], casaRei[0], casaRei[1]);
            bool chequeDeCavalo2 = CondiçãoDeMovimentoDoCavalo(casaCavalo2[0], casaCavalo2[1], casaRei[0], casaRei[1]);
            bool chequeDeBispo1 = CondiçãoDeMovimentoDoBispo(casaBispo1[0], casaBispo1[1], casaRei[0], casaRei[1], tabuleiro);
            bool chequeDeBispo2 = CondiçãoDeMovimentoDoBispo(casaBispo2[0], casaBispo2[1], casaRei[0], casaRei[1], tabuleiro);
            bool chequeDeRainha = CondiçãoDeMovimentoDoBispo(casaRainha[0], casaRainha[1], casaRei[0], casaRei[1], tabuleiro) ||
                 CondiçãoDeMovimentoDaTorre(casaRainha[0], casaRainha[1], casaRei[0], casaRei[1], tabuleiro);

            if (chequeDeTorre1 || chequeDeTorre2 || chequeDeCavalo1 || chequeDeCavalo2 || chequeDeBispo1 || chequeDeBispo2 || chequeDeRainha)
            {

                string message = corDaPeça == "branca" ? "O rei branco está em cheque " : "O rei preto está em cheque";
                Console.WriteLine(message);
            }




        }

        public int ChecarSeReiFoiCapturado(string corDaPeça, string[,] tabuleiro, string player1, string player2)
        {
            int posiçãoAbsolutaReiBranco = posiçãoPeçasBrancas[3];
            int[] casaReiBranco = RetornarPosiçãoNoTabuleiro(posiçãoAbsolutaReiBranco);
            int posiçãoAbsolutaReiPreto = posiçãoPeçasPretas[3];
            int[] casaReiPreto = RetornarPosiçãoNoTabuleiro(posiçãoAbsolutaReiPreto);


            if (tabuleiro[casaReiBranco[0], casaReiBranco[1]] != "\x2654")
            {
                Console.WriteLine($"Xeque Mate!, o jogador {player2} venceu");
                Console.WriteLine("Aperte Qualquer tecla para Continuar");
                Console.ReadKey();
                return 2;
            }
            else if (tabuleiro[casaReiPreto[0], casaReiPreto[1]] != "\x265A")
            {
                Console.WriteLine($"Xeque Mate!, o jogador {player1} venceu");
                Console.WriteLine("Aperte Qualquer tecla para Continuar");
                Console.ReadKey();
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public void AtribuirResultadoXadrez(string player1, string player2, int vencedor, string fileName)
        {
            Jogador jogador1 = Jogadores.Find(player => player.Usuario == player1);
            Jogador jogador2 = Jogadores.Find(player => player.Usuario == player2);
            switch (vencedor)
            {
                case 1:
                    jogador1.DadosXadrez.VitoriasXadrez++;
                    jogador2.DadosXadrez.DerrotasXadrez++;
                    break;
                case 2:

                    jogador2.DadosXadrez.VitoriasXadrez++;
                    jogador1.DadosXadrez.DerrotasXadrez++;
                    break;
                case 3:
                    jogador1.DadosXadrez.EmpatesXadrez++;
                    jogador2.DadosXadrez.EmpatesXadrez++;
                    break;
            }
            Jogadores.ForEach(jogador => jogador.DadosXadrez.ObterPontuacaoXadrez(jogador.DadosXadrez.VitoriasXadrez, jogador.DadosXadrez.EmpatesXadrez, jogador.DadosXadrez.DerrotasXadrez));
            string writeJsonString = JsonSerializer.Serialize(Jogadores);
            File.WriteAllText(fileName, writeJsonString);
        }



    }
}
