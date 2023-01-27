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
    public class Xadrez : GameHub
    {
        /*
         Ler o tabueleiro,e pegar as posições atuais das peças*/
        public Xadrez() { }

        private Peça _peça { get; set; } = new Peça();

        private string _peaoPreto = "\x265F";
        private string _peaoBranco = "\x2659";
        private string[] _peçasPretas = { "\x265C", "\x265E", "\x265D", "\x265B", "\x265A", "\x265D", "\x265E", "\x265C" };
        private string[] _peçasBrancas = { "\x2656", "\x2658", "\x2657", "\x2655", "\x2654", "\x2657", "\x2658", "\x2656" };
        private int[] posiçãoPeçasPretas = { 1, 2, 3, 4, 5, 6, 7, 8 };//{Torre,Cavalo,Bispo,Rainha,Rei,,Bispo,Cavalo,Torre}
        private int[] posiçãoPeçasBrancas = { 57, 58, 59, 60, 61, 62, 63, 64 };//{Torre,Cavalo,Bispo,Rainha,Rei,Bispo,Cavalo,Torre}
        private double _rodada = 1;
        private string _stringPgn = "";

        public int[] RetornarPosiçãoNoTabuleiro(int posiçãoAbsoluta)
        {
            int linha = (posiçãoAbsoluta - 1) / 8;
            int coluna = posiçãoAbsoluta - linha * 8 - 1;

            int[] coords = { linha, coluna };

            return coords;
        }

        public void ChecagemParaGerarStringPgn(string[,] tabuleiro, int[] casaFinal, string casaStr, string corDaPeça, string tipoDePeça)
        {
            if (tabuleiro[casaFinal[0], casaFinal[1]] == " ")
            {
                _stringPgn += corDaPeça == "branca" ? $"{_rodada}. {tipoDePeça}{casaStr}" : $"{tipoDePeça}{casaStr}";
            }
            else
            {
                _stringPgn += corDaPeça == "branca" ? $"{_rodada}. {tipoDePeça}x{casaStr}" : $"{tipoDePeça}x{casaStr}";
            }
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

        public int[] ValidarInputDeDados(string casaStr)
        {
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

            int count = 1;
            for (int i = 8; i >= 1; i--)
            {
                if (linha == i)
                {
                    linha = count - 1;
                    break;
                }
                count++;
            }

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

        public bool ChecarSeleçãoDePeçaAdversaria(string corDaPeça, int[] casaInicial, string[,] tabuleiro)
        {
            if (corDaPeça == "branca")
            {

                if (_peçasPretas.Contains(tabuleiro[casaInicial[0], casaInicial[1]]) || tabuleiro[casaInicial[0], casaInicial[1]] == _peaoPreto)
                {
                    return true;
                }
            }
            else
            {
                if (_peçasBrancas.Contains(tabuleiro[casaInicial[0], casaInicial[1]]) || tabuleiro[casaInicial[0], casaInicial[1]] == _peaoBranco)
                {
                    return true;
                }
            }
            return false;
        }

        public string[,] CriarTabuleiro()
        {
            Console.Clear();
            string[,] tabuleiro = new string[8, 8];
            int num = 8;

            Console.WriteLine(new string('-', 27));
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{num} ");
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
                num--;
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.WriteLine(new string('-', 27));

            Console.WriteLine("Turorial:");
            Console.WriteLine("");
            Console.WriteLine(" Para jogar, basta digitar a posição da peça que deseja movimentar,como mostradado no tabuleiro");
            Console.WriteLine(" Entao digite a posição da casa para qual deseja mover sua peça");
            Console.WriteLine(" Para se render basta digitar o numero 0");
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
            return tabuleiro;
        }

        public string[,] MostrarTabuleiro(string[,] tabuleiro)
        {
            Console.Clear();
            Console.WriteLine(new string('-', 27));
            int num = 8;
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{num} ");
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
                num--;
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
                    colunaInicial++;
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
                    colunaInicial--;
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

        public string[,] MoverPeao(string corDaPeça, int[] casaInicial, string[,] tabuleiro, string letraInicial)
        {
            Console.WriteLine("Selecione a casa para qual deseja mover sua peça");
            string casaStr = Console.ReadLine();
            int[] casaFinal = ValidarInputDeDados(casaStr);

            if (tabuleiro[casaFinal[0], casaFinal[1]] == " ")
            {
                _stringPgn += corDaPeça == "branca" ? $"{_rodada}. {casaStr}" : $"{casaStr}";
            }
            else
            {
                _stringPgn += corDaPeça == "branca" ? $"{_rodada}. {letraInicial}x{casaStr}" : $"{letraInicial}x{casaStr}";

            }


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
                        casaStr = Console.ReadLine();
                        casaFinal = ValidarInputDeDados(casaStr);
                    }

                }
                if (!estaDentroDoTabuleiro)
                {
                    Console.WriteLine("Posição invalida, por favor digite novamente ");
                    casaStr = Console.ReadLine();
                    casaFinal = ValidarInputDeDados(casaStr);
                }


            }
            while (true);




            return tabuleiro;
        }
        public string[,] MoverPeça(string corDaPeça, int[] casaInicial, string[,] tabuleiro, string peça)
        {

            Console.WriteLine("Selecione a casa para qual deseja mover sua peça");

            string casaStr = Console.ReadLine();
            int[] casaFinal = ValidarInputDeDados(casaStr);

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
                        case "Torre":
                            movimentoÉvalido = CondiçãoDeMovimentoDaTorre(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro);
                            ChecagemParaGerarStringPgn(tabuleiro, casaFinal, casaStr, corDaPeça, "R");
                            break;
                        case "Cavalo":
                            movimentoÉvalido = CondiçãoDeMovimentoDoCavalo(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial);
                            ChecagemParaGerarStringPgn(tabuleiro, casaFinal, casaStr, corDaPeça, "N");
                            break;
                        case "Bispo":
                            movimentoÉvalido = CondiçãoDeMovimentoDoBispo(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro);
                            ChecagemParaGerarStringPgn(tabuleiro, casaFinal, casaStr, corDaPeça, "B");
                            break;
                        case "Rei":
                            movimentoÉvalido = CondiçãoDeMovimentoDoRei(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial);
                            ChecagemParaGerarStringPgn(tabuleiro, casaFinal, casaStr, corDaPeça, "K");
                            break;
                        case "Rainha":
                            movimentoÉvalido = CondiçãoDeMovimentoDaTorre(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro) || CondiçãoDeMovimentoDoBispo(casaFinal[0], casaFinal[1], linhaInicial, colunaInicial, tabuleiro);
                            ChecagemParaGerarStringPgn(tabuleiro, casaFinal, casaStr, corDaPeça, "Q");
                            break;
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
                        casaStr = Console.ReadLine();
                        casaFinal = ValidarInputDeDados(casaStr);
                    }

                }
                if (!estaDentroDoTabuleiro)
                {
                    Console.WriteLine("Posição invalida, por favor digite novamente ");
                    casaStr = Console.ReadLine();
                    casaFinal = ValidarInputDeDados(casaStr);
                }


            }
            while (true);




            return tabuleiro;


        }
        public void InicializarJogo(string fileName, Jogador[] jogadores)
        {

            int vencedor = JogarXadrez(jogadores[0].Usuario, jogadores[1].Usuario);

            GerarArquivoPGN(vencedor);
            AtribuirResultadoDeJogo(jogadores[0], jogadores[1], vencedor, fileName, "xadrez");

        }

        public int JogarXadrez(string player1, string player2)
        {
            int vezDoJogador = 1;
            int vencedor;
            bool opçaoValida;
            string corDaPeça;
            string[,] tabuleiro = CriarTabuleiro();

            do
            {
                tabuleiro = MostrarTabuleiro(tabuleiro);

                corDaPeça = vezDoJogador == 1 ? "branca" : "preta";
                string nomeDoJogador = vezDoJogador == 1 ? player1 : player2;


                Console.WriteLine($"Vez do jogador {nomeDoJogador}, selecione a casa da peça que deseja mover");

                string? casaStr;
                string letraInicial = "";
                int[] casaInicial = new int[2];
                string peça = "";
                do
                {
                    casaStr = Console.ReadLine();

                    if (casaStr == "0")
                    {

                        vencedor = vezDoJogador == 1 ? 2 : 1;
                        string vitorioso = vencedor == 1 ? player1 : player2;
                        string perdedor = vencedor == 1 ? player2 : player1;
                        Console.WriteLine($"O jogador {perdedor} desistiu, o vencedor é {vitorioso} ");
                        return vencedor;
                    }


                    if (String.IsNullOrEmpty(casaStr) || casaStr.Length != 2)
                    {
                        Console.WriteLine("Casa não valida, digite uma casa valida");
                    }
                    else
                    {
                        casaInicial = ValidarInputDeDados(casaStr);
                        letraInicial = casaStr[0].ToString();
                        peça = tabuleiro[casaInicial[0], casaInicial[1]];
                    }

                    if (ChecarSeleçãoDePeçaAdversaria(corDaPeça, casaInicial, tabuleiro))
                    {
                        Console.WriteLine("Voce selecionou uma peça adversaria, digite uma peça valida");
                    }

                } while (String.IsNullOrEmpty(casaStr) || casaStr.Length != 2 || ChecarSeleçãoDePeçaAdversaria(corDaPeça, casaInicial, tabuleiro));


                while (true)
                {

                    switch (peça)
                    {
                        case "\x2659" or "\x265F"://Peao
                            tabuleiro = MoverPeao(corDaPeça, casaInicial, tabuleiro, letraInicial);
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
                        case "\x2654" or "\x265A"://Rei
                            tabuleiro = MoverPeça(corDaPeça, casaInicial, tabuleiro, "Rei");
                            opçaoValida = true;
                            break;
                        case "\x265B" or "\x2655"://Rainha
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
                        casaStr = Console.ReadLine();
                        casaInicial = ValidarInputDeDados(casaStr);

                        peça = tabuleiro[casaInicial[0], casaInicial[1]];
                    }
                }
                _rodada += 0.5;
                bool xeque = ChecarXeque(corDaPeça, tabuleiro);

                vencedor = ChecarSeReiFoiCapturado(tabuleiro, player1, player2);


                if (vencedor == 1 || vencedor == 2)
                {
                    return vencedor;
                }

                _stringPgn += xeque ? "+ " : " ";
                vezDoJogador = TrocarJogador(vezDoJogador);

            } while (true);



        }

        public bool ChecarXeque(string corDaPeça, string[,] tabuleiro)
        {

            string corDoOponente = corDaPeça == "branca" ? "preta" : "branca";

            int posiçãoRei = corDoOponente == "branca" ? posiçãoPeçasBrancas[4] : posiçãoPeçasPretas[4];
            int[] casaRei = RetornarPosiçãoNoTabuleiro(posiçãoRei);


            int posiçãoTorre1 = corDaPeça == "branca" ? posiçãoPeçasBrancas[0] : posiçãoPeçasPretas[0];
            int[] casaTorre1 = RetornarPosiçãoNoTabuleiro(posiçãoTorre1);
            int posiçãoTorre2 = corDaPeça == "branca" ? posiçãoPeçasBrancas[7] : posiçãoPeçasPretas[7];
            int[] casaTorre2 = RetornarPosiçãoNoTabuleiro(posiçãoTorre2);
            int posiçãoCavalo1 = corDaPeça == "branca" ? posiçãoPeçasBrancas[1] : posiçãoPeçasPretas[1];
            int[] casaCavalo1 = RetornarPosiçãoNoTabuleiro(posiçãoCavalo1);
            int posiçãoCavalo2 = corDaPeça == "branca" ? posiçãoPeçasBrancas[6] : posiçãoPeçasPretas[6];
            int[] casaCavalo2 = RetornarPosiçãoNoTabuleiro(posiçãoCavalo2);
            int posiçãoBispo1 = corDaPeça == "branca" ? posiçãoPeçasBrancas[2] : posiçãoPeçasPretas[2];
            int[] casaBispo1 = RetornarPosiçãoNoTabuleiro(posiçãoBispo1);
            int posiçãoBispo2 = corDaPeça == "branca" ? posiçãoPeçasBrancas[5] : posiçãoPeçasPretas[5];
            int[] casaBispo2 = RetornarPosiçãoNoTabuleiro(posiçãoBispo2);
            int posiçãoRainha = corDaPeça == "branca" ? posiçãoPeçasBrancas[3] : posiçãoPeçasPretas[3];
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

                string message = corDoOponente == "branca" ? "O rei branco está em cheque " : "O rei preto está em cheque";
                Console.WriteLine(message);
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                return true;
            }

            //ChecarXequeMate(casaRei[0] + 1, casaRei[1]);
            //ChecarXequeMate(casaRei[0] - 1, casaRei[1]);
            //ChecarXequeMate(casaRei[0], casaRei[1] + 1);
            //ChecarXequeMate(casaRei[0], casaRei[1] - 1);
            //ChecarXequeMate(casaRei[0] + 1, casaRei[1] + 1);
            //ChecarXequeMate(casaRei[0] + 1, casaRei[1] - 1);
            //ChecarXequeMate(casaRei[0] - 1, casaRei[1] + 1);
            //ChecarXequeMate(casaRei[0] - 1, casaRei[1] - 1);



            return false;


        }
        public int ChecarSeReiFoiCapturado(string[,] tabuleiro, string player1, string player2)
        {
            int posiçãoAbsolutaReiBranco = posiçãoPeçasBrancas[4];
            int[] casaReiBranco = RetornarPosiçãoNoTabuleiro(posiçãoAbsolutaReiBranco);
            int posiçãoAbsolutaReiPreto = posiçãoPeçasPretas[4];
            int[] casaReiPreto = RetornarPosiçãoNoTabuleiro(posiçãoAbsolutaReiPreto);


            if (tabuleiro[casaReiBranco[0], casaReiBranco[1]] != "\x2654")
            {
                // _stringPgn.Remove(_stringPgn.Length-4);
                Console.WriteLine($"Fim de jogo!, o jogador {player2} venceu");
                Console.WriteLine("Aperte Qualquer tecla para Continuar");
                Console.ReadKey();
                return 2;
            }
            else if (tabuleiro[casaReiPreto[0], casaReiPreto[1]] != "\x265A")
            {
                // _stringPgn.Remove(_stringPgn.Length-4);
                Console.WriteLine($"Fim de jogo!, o jogador {player1} venceu");
                Console.WriteLine("Aperte Qualquer tecla para Continuar");
                Console.ReadKey();
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public void GerarArquivoPGN(int vencedor)
        {
            int p1 = vencedor == 1 ? 1 : 0;
            int p2 = vencedor == 2 ? 1 : 0;

            _stringPgn += $" {p1}-{p2}";

            string[] camposPGN = { "[Event Xadrez Sharp Coders]", "[ Site Belo Horizonte, MG BRA]", "[Date 2023.01.22]", $"[Round ?]", "[White Gabriel]", "[Black Arthur]", $"[Result {p1}-{p2}]" };




            string filePath = @"..\..\..\..\pgn\xadrez.pgn";

            File.Create(filePath).Close();


            using StreamWriter sw = new StreamWriter(filePath);
            foreach (string campo in camposPGN)
            {
                sw.WriteLine(campo);
            }
            sw.WriteLine(_stringPgn);
        }




    }
}
