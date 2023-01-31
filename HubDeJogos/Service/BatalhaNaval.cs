using System.Data.SqlTypes;
using Hub.Model.batalhaNaval;
using Hub.Utils;
using Hub.Model;

namespace Hub.Service
{
    public class BatalhaNaval : GameHub
    {
        public string StatusDoAtaque { get; set; }
        public Navio Navio { get; set; }
        public BatalhaNaval() { }

        public Navio[] GerarNavios()
        {

            Navio[] naviosPlayer1 = {
                new Navio(5, "Porta Avioes","pa"),
                new Navio(4, "Navio Grande","g1"),
                new Navio(4, "Navio Grande","g2"),
                new Navio(3, "Navio Medio","m1"),
                 new Navio(3, "Navio Medio","m2"),
                 new Navio(2, "Navio Pequeno","p1"),
                 new Navio(2, "Navio Pequeno","p2"),
                 new Navio(2, "Navio Pequeno","p3"),
        };
            return naviosPlayer1;

        }

        public int[] ValidarInputDeDados(string casaAlvoStr, string[,] tabuleiro)
        {
            int[] casaAlvo = TranformarStringParaCoords(casaAlvoStr);

            while (true)
            {
                if (casaAlvo[1] == -1 || casaAlvo[0] < 0 || casaAlvo[0] > 9)
                {
                    Console.WriteLine("Casa não valida, digite uma casa valida");
                    casaAlvoStr = Console.ReadLine();
                    casaAlvo = TranformarStringParaCoords(casaAlvoStr);
                }
                else if (tabuleiro[casaAlvo[0], casaAlvo[1]].EndsWith("0"))
                {
                    Console.WriteLine("Casa não valida, digite uma casa valida");
                    casaAlvoStr = Console.ReadLine();
                    casaAlvo = TranformarStringParaCoords(casaAlvoStr);
                }
                else
                {
                    break;
                }
            }


            return casaAlvo;
        }

        public int[] TranformarStringParaCoords(string casaAlvoStr)
        {

            int linha;
            int coluna = -1;
            if (casaAlvoStr.Length == 3 && casaAlvoStr[1].ToString() == "1" && casaAlvoStr[2].ToString() == "0")
            {
                linha = 9;
            }
            else
            {
                linha = int.Parse(casaAlvoStr[1].ToString()) - 1;
            }

            string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

            string colunaStr = casaAlvoStr[0].ToString();
            for (int i = 0; i < 10; i++)
            {
                if (colunaStr == letras[i])
                {
                    coluna = i;
                    break;
                }
            }

            if (casaAlvoStr.Length >= 3 && linha != 9)
            {
                coluna = -1;
            }

            int[] casaAlvo = { linha, coluna };
            return casaAlvo;

        }

        public void MostrarTabuleiro(string[,] tabuleiro, Navio[] naviosDoOponente, string nomeDoJogador)
        {
            Console.Clear();
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"           Frota de {nomeDoJogador}         ");
            Console.WriteLine(" | a | b | c | d | e | f | g | h | i | j |");

            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0,-2}", i + 1);
                for (int j = 0; j < 10; j++)
                {

                    if (tabuleiro[i, j] == "a0")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write("    ");
                        Console.ResetColor();
                    }
                    else if (tabuleiro[i, j].EndsWith("0"))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        // Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(" 💥 ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write(" ~  ");
                        Console.ResetColor();
                    }
                }
                Console.Write("\n");
            }
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Porta Avioes : {(naviosDoOponente[0].Integridade > 0 ? "Em combate" : "Afundado")}");
            Console.WriteLine($"Navio Grande 1 : {(naviosDoOponente[1].Integridade > 0 ? "Em combate" : "Afundado")}");
            Console.WriteLine($"Navio Grande 2 : {(naviosDoOponente[2].Integridade > 0 ? "Em combate" : "Afundado")}");
            Console.WriteLine($"Navio Medio 1 : {(naviosDoOponente[3].Integridade > 0 ? "Em combate" : "Afundado")}");
            Console.WriteLine($"Navio Medio 2 : {(naviosDoOponente[4].Integridade > 0 ? "Em combate" : "Afundado")}");
            Console.WriteLine($"Navio Pequeno 1 : {(naviosDoOponente[5].Integridade > 0 ? "Em combate" : "Afundado")}");
            Console.WriteLine($"Navio Pequeno 2 : {(naviosDoOponente[6].Integridade > 0 ? "Em combate" : "Afundado")}");
            Console.WriteLine($"Navio Pequeno 3 : {(naviosDoOponente[7].Integridade > 0 ? "Em combate" : "Afundado")}");
            Console.WriteLine(new string('-', 50));
        }

        public void InicializarJogo(string fileName, Jogador[] jogadores)
        {

            int vencedor = JogarBatalhaNaval(jogadores[0].Usuario, jogadores[1].Usuario);

            AtribuirResultadoDeJogo(jogadores[0].DadosNaval, jogadores[1].DadosNaval, vencedor, fileName);


        }

        public int JogarBatalhaNaval(string player1, string player2)
        {

            Navio[] naviosPlayer1 = GerarNavios();
            Navio[] naviosPlayer2 = GerarNavios();


            string[,] tabuleiro1 = GerarTabuleiroAleatorio();
            string[,] tabuleiro2 = GerarTabuleiroAleatorio();
            string[,] tabuleiroDoOponente;
            int vezDoJogador = 1;
            int vencedor;
            string casaAlvoStr = "";

            do
            {

                int[] casaAlvo = new int[2];
                string nomeDoJogadorDaVez = vezDoJogador == 1 ? player1 : player2;
                string nomeDoOponente = vezDoJogador == 1 ? player2 : player1;

                tabuleiroDoOponente = vezDoJogador == 1 ? tabuleiro2 : tabuleiro1;
                Navio[] naviosDoOponente = vezDoJogador == 1 ? naviosPlayer2 : naviosPlayer1;

                MostrarTabuleiro(tabuleiroDoOponente, naviosDoOponente, nomeDoOponente);

                Console.WriteLine($"Vez do jogador {nomeDoJogadorDaVez}, Digite a casa que deseja atirar");

                do
                {
                    try
                    {

                        casaAlvoStr = Console.ReadLine();
                        casaAlvo = ValidarInputDeDados(casaAlvoStr, tabuleiroDoOponente);
                    }
                    catch (IndexOutOfRangeException indexOut)
                    {
                        Console.WriteLine(indexOut.Message);
                        Console.WriteLine("Casa nao valida, Digite novamente");
                    }
                } while (casaAlvoStr.Length < 2);



                tabuleiroDoOponente = AtacarAlvo(tabuleiroDoOponente, naviosDoOponente, casaAlvo);
                MostrarTabuleiro(tabuleiroDoOponente, naviosDoOponente, nomeDoOponente);

                Console.WriteLine(StatusDoAtaque);
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                bool isGameWon = ChecarCondiçãoDeVitoria(naviosDoOponente);
                vencedor = isGameWon ? vezDoJogador : 0;
                if (vencedor != 0) Console.WriteLine($"Fim de Jogo!,O jogador {nomeDoJogadorDaVez} é o vencedor");


                vezDoJogador = Helpers.TrocarJogador(vezDoJogador);



            } while (vencedor == 0);
            ;
            return vencedor;

        }

        public bool ChecarCondiçãoDeVitoria(Navio[] navios)
        {
            foreach (Navio navio in navios)
            {
                if (navio.Integridade > 0)
                {
                    return false;
                }
            }
            return true;


        }

        public string[,] AtacarAlvo(string[,] tabuleiroDoOponente, Navio[] naviosDoOponente, int[] casaAlvo)
        {

            switch (tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]])
            {
                case "a":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "a0";
                    StatusDoAtaque = "O missil caiu na água";
                    return tabuleiroDoOponente;
                case "pa":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "pa0";
                    naviosDoOponente[0].Integridade--;
                    StatusDoAtaque = $"O missil acertou um navio!, {(naviosDoOponente[0].Integridade == 0 ? $"e afundou um {naviosDoOponente[0].Tipo} do oponente" : "")}";
                    return tabuleiroDoOponente;
                case "g1":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "g10";
                    naviosDoOponente[1].Integridade--;
                    StatusDoAtaque = $"O missil acertou um navio!, {(naviosDoOponente[1].Integridade == 0 ? $"e afundou um {naviosDoOponente[1].Tipo} do oponente" : "")}";
                    return tabuleiroDoOponente;
                case "g2":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "g20";
                    naviosDoOponente[2].Integridade--;
                    StatusDoAtaque = $"O missil acertou um navio!, {(naviosDoOponente[2].Integridade == 0 ? $"e afundou um {naviosDoOponente[2].Tipo} do oponente" : "")}";
                    return tabuleiroDoOponente;
                case "m1":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "m10";
                    naviosDoOponente[3].Integridade--;
                    StatusDoAtaque = $"O missil acertou um navio!, {(naviosDoOponente[3].Integridade == 0 ? $"e afundou um {naviosDoOponente[3].Tipo} do oponente" : "")}";
                    return tabuleiroDoOponente;
                case "m2":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "m20";
                    naviosDoOponente[4].Integridade--;
                    StatusDoAtaque = $"O missil acertou um navio!, {(naviosDoOponente[4].Integridade == 0 ? $"e afundou um {naviosDoOponente[4].Tipo} do oponente" : "")}";
                    return tabuleiroDoOponente;
                case "p1":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "p10";
                    naviosDoOponente[5].Integridade--;
                    StatusDoAtaque = $"O missil acertou um navio!, {(naviosDoOponente[5].Integridade == 0 ? $"e afundou um {naviosDoOponente[5].Tipo} do oponente" : "")}";
                    return tabuleiroDoOponente;
                case "p2":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "p20";
                    naviosDoOponente[6].Integridade--;
                    StatusDoAtaque = $"O missil acertou um navio!, {(naviosDoOponente[6].Integridade == 0 ? $"e afundou um {naviosDoOponente[6].Tipo} do oponente" : "")}";
                    return tabuleiroDoOponente;
                case "p3":
                    tabuleiroDoOponente[casaAlvo[0], casaAlvo[1]] = "p30";
                    naviosDoOponente[7].Integridade--;
                    StatusDoAtaque = $"O missil acertou um navio!, {(naviosDoOponente[7].Integridade == 0 ? $"e afundou um {naviosDoOponente[7].Tipo} do oponente" : "")}";
                    return tabuleiroDoOponente;
            }
            return tabuleiroDoOponente;
        }
        public string[,] GerarTabuleiroAleatorio()
        {
            Navio[] navios = GerarNavios();
            string[,] jogo = new string[10, 10];

            Random rd = new Random();

            foreach (Navio navio in navios)
            {

                int orientação = rd.Next(1, 3);


                if (orientação == 1)//horizontal
                {
                    int linha = rd.Next(0, 10);
                    int coluna = rd.Next(0, 10 - navio.Integridade);
                    int colunaInicial = coluna;
                    int casasLivres = 0;

                    do
                    {
                        for (int i = 0; i < navio.Integridade; i++)
                        {
                            if (jogo[linha, coluna] == null)
                            {
                                casasLivres++;

                            }
                            coluna++;
                        }

                        if (casasLivres < navio.Integridade)
                        {
                            linha = rd.Next(0, 10);
                            coluna = rd.Next(0, 10 - navio.Integridade);
                            colunaInicial = coluna;
                            casasLivres = 0;
                        }
                        else
                        {
                            break;
                        }

                    } while (true);


                    //Somente se o codigo chegar aqui ele preenche o navio
                    for (int i = 0; i < navio.Integridade; i++)
                    {
                        jogo[linha, colunaInicial] = navio.Abreviatura;
                        colunaInicial++;
                    }

                }
                else if (orientação == 2)//vertical
                {
                    int linha = rd.Next(0, 10 - navio.Integridade);
                    int linhaInicial = linha;
                    int coluna = rd.Next(0, 10);
                    int casasLivres = 0;

                    do
                    {
                        for (int i = 0; i < navio.Integridade; i++)
                        {
                            if (jogo[linha, coluna] == null)
                            {
                                casasLivres++;

                            }
                            linha++;
                        }

                        if (casasLivres < navio.Integridade)
                        {
                            linha = rd.Next(0, 10 - navio.Integridade);
                            linhaInicial = linha;
                            coluna = rd.Next(0, 10);
                            casasLivres = 0;
                        }
                        else
                        {
                            break;
                        }

                    } while (true);

                    //Somente se o codigo chegar aqui ele preenche o navio
                    for (int i = 0; i < navio.Integridade; i++)
                    {
                        jogo[linhaInicial, coluna] = navio.Abreviatura;
                        linhaInicial++;
                    }
                }
            }

            //Preencher as casas restantes com agua
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (jogo[i, j] == null)
                    {
                        jogo[i, j] = "a";
                    }
                }
            }

            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        Console.Write($"{jogo[i, j]} ");
            //    }
            //    Console.Write("\n");
            //}
            //Console.ReadKey();

            return jogo;


        }

    }
}