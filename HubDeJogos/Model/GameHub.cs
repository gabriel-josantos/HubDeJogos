
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hub.Model
{
    public class GameHub
    {
        static public List<Jogador> Jogadores { get; protected set; }

        public GameHub()
        {
            Jogadores = new List<Jogador>();
        }

        public void SerializarJson(string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(Jogadores, options);
            File.WriteAllText(fileName, jsonString);
        }
        public void LerArquivoJsonDeJogadores(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            List<Jogador?> allPlayers = JsonSerializer.Deserialize<List<Jogador?>>(jsonString);
            allPlayers.ForEach(player => Jogadores.Add(player));
        }

        public void CadastrarNovoJogador(string fileName)
        {
            Console.WriteLine("Insira o nome de usuario");
            string usuario = Console.ReadLine();

            if (ChecarExistenciaDeJogador(usuario))
            {
                Console.WriteLine("Este nome de usuario ja esta sendo utilizado, por favor escolha outro");
            }
            else
            {
                Console.WriteLine("Digite uma senha");
                string senha = Console.ReadLine();
                Jogador jogador = new Jogador(usuario, senha);
                Jogadores.Add(jogador);
                Console.WriteLine($"usuario {usuario} criado com sucesso");
            }
            SerializarJson(fileName);
        }

        public void MostrarJogadoresCadastrados()
        {

            Jogadores.ForEach(player => Console.WriteLine($"Usuario : {player.Usuario}"));

        }

        public void AtualizarDadosDeJogador(string fileName)
        {
            Console.WriteLine("Digite o usuario do jogador que deseja atualizar");
            string jogadorParaAtualizar = Console.ReadLine();
            if (ChecarExistenciaDeJogador(jogadorParaAtualizar))
            {
                Jogador jogador = EncontrarJogador(jogadorParaAtualizar);
                Console.WriteLine("Digite 1 para atualizar o usuario");
                Console.WriteLine("Digite 2 para atualizar a senha");
                int updateOpt = int.Parse(Console.ReadLine());
                if (updateOpt == 1)
                {
                    while (true)
                    {
                        Console.WriteLine("Digite o novo nome de usuario");
                        string novoUsuario = Console.ReadLine();
                        if (!ChecarExistenciaDeJogador(novoUsuario))
                        {
                            jogador.Usuario = novoUsuario;
                            SerializarJson(fileName);
                            Console.WriteLine("Nome de usuario atualizado com sucesso!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Este nome de usuario ja esta sendo utilizidado, por favor escolha outro");
                        }
                    }


                }
                else if (updateOpt == 2)
                {
                    Console.WriteLine("Digite o novo nome do jogador");
                    string novaSenha = Console.ReadLine();
                    jogador.Senha = novaSenha;
                    SerializarJson(fileName);
                    Console.WriteLine("Nome de jogador atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Opção invalida");
                }

            }
            else
            {
                Console.WriteLine("Nenhum jogador possuei esse usuario");
            }
        }

        public void DeletarJogador(string fileName)
        {
            Console.WriteLine("Digite o usuario do jogador que deseja deletar");
            string jogadorParaDeletar = Console.ReadLine();
            if (ChecarExistenciaDeJogador(jogadorParaDeletar))
            {
                Jogador jogador = EncontrarJogador(jogadorParaDeletar);
                Jogadores.Remove(jogador);
                SerializarJson(fileName);
            }
            else
            {
                Console.WriteLine("Nenhum jogador possuei este usuario");
            }

        }

        public bool ChecarExistenciaDeJogador(string usuario)
        {
            return Jogadores.Exists(jogador => jogador.Usuario == usuario);
        }
        public Jogador EncontrarJogador(string usuario)
        {
            return Jogadores.Find(jogador => jogador.Usuario == usuario);
        }

        public int TrocarJogador(int vezDoJogador)
        {
            vezDoJogador = vezDoJogador == 1 ? 2 : 1;
            return vezDoJogador;
        }

        public void AtribuirResultadoDeJogo(Jogador jogador1, Jogador jogador2, int vencedor, string fileName, string tipoDeJogo)
        {

            if (tipoDeJogo == "xadrez")
            {
                switch (vencedor)
                {
                    case 1:
                        jogador1.DadosXadrez.Vitorias++;
                        jogador2.DadosXadrez.Derrotas++;
                        break;
                    case 2:
                        jogador2.DadosXadrez.Vitorias++;
                        jogador1.DadosXadrez.Derrotas++;
                        break;
                    case 3:
                        jogador2.DadosXadrez.Empates++;
                        jogador1.DadosXadrez.Empates++;
                        break;
                }

                Jogadores.ForEach(jogador => jogador.DadosXadrez.ObterPontuacao(jogador.DadosXadrez.Vitorias, jogador.DadosXadrez.Empates, jogador.DadosXadrez.Derrotas));
                SerializarJson(fileName);
            }

            if (tipoDeJogo == "velha")
            {
                switch (vencedor)
                {
                    case 1:
                        jogador1.DadosVelha.Vitorias++;
                        jogador2.DadosVelha.Derrotas++;
                        break;
                    case 2:
                        jogador2.DadosVelha.Vitorias++;
                        jogador1.DadosVelha.Derrotas++;
                        break;
                    case 3:
                        jogador2.DadosVelha.Empates++;
                        jogador1.DadosVelha.Empates++;
                        break;
                }

                Jogadores.ForEach(jogador => jogador.DadosVelha.ObterPontuacao(jogador.DadosVelha.Vitorias, jogador.DadosVelha.Empates, jogador.DadosVelha.Derrotas));
                SerializarJson(fileName); ;
            }
            if (tipoDeJogo == "naval")
            {
                switch (vencedor)
                {
                    case 1:
                        jogador1.DadosNaval.Vitorias++;
                        jogador2.DadosNaval.Derrotas++;
                        break;
                    case 2:
                        jogador2.DadosNaval.Vitorias++;
                        jogador1.DadosNaval.Derrotas++;
                        break;
                    case 3:
                        jogador2.DadosNaval.Empates++;
                        jogador1.DadosNaval.Empates++;
                        break;
                }

                Jogadores.ForEach(jogador => jogador.DadosNaval.ObterPontuacao(jogador.DadosNaval.Vitorias, jogador.DadosNaval.Empates, jogador.DadosNaval.Derrotas));
                SerializarJson(fileName); ;
            }
        }

        public void MostrarRankingDeJogo(string tipoDeJogo)
        {
            if (tipoDeJogo == "velha")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Clear();
                int[] pontosDeJogadores = new int[Jogadores.Count];
                List<string> usuariosCopia = new List<string>();
                for (int i = 0; i < Jogadores.Count; i++)
                {
                    pontosDeJogadores[i] = Jogadores[i].DadosVelha.Pontuacao;
                    usuariosCopia.Add(Jogadores[i].Usuario);
                }
                Array.Sort(pontosDeJogadores);
                Array.Reverse(pontosDeJogadores);
                Console.WriteLine("-----------------------TOP-10-----------------------");
                Console.WriteLine("| Jogador | Pontos | Vitorias | Derrotas | Empates |");

                for (int j = 0; j < 10; j++)
                {
                    //achar o index do player correspondete a primeira pontuação no vetor pontos
                    //Dificil
                    Jogador jogador = Jogadores.Find(player => player.DadosVelha.Pontuacao == pontosDeJogadores[j] && usuariosCopia.Exists(jogador => player.Usuario == jogador));
                    Console.WriteLine("|{0,9}|{1,8}|{2,10}|{3,10}|{4,9}|", jogador.Usuario, pontosDeJogadores[j], jogador.DadosVelha.Vitorias, jogador.DadosVelha.Derrotas, jogador.DadosVelha.Empates);
                    usuariosCopia.Remove(jogador.Usuario);

                }
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("");
            }
            if (tipoDeJogo == "xadrez")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Clear();
                int[] pontosDeJogadores = new int[Jogadores.Count];
                List<string> usuariosCopia = new List<string>();
                for (int i = 0; i < Jogadores.Count; i++)
                {
                    pontosDeJogadores[i] = Jogadores[i].DadosXadrez.Pontuacao;
                    usuariosCopia.Add(Jogadores[i].Usuario);
                }
                Array.Sort(pontosDeJogadores);
                Array.Reverse(pontosDeJogadores);
                Console.WriteLine("-----------------------TOP-10-----------------------");
                Console.WriteLine("| Jogador | Pontos | Vitorias | Derrotas | Empates |");

                for (int j = 0; j < 10; j++)
                {
                    //achar o index do player correspondete a primeira pontuação no vetor pontos
                    //Dificil
                    Jogador jogador = Jogadores.Find(player => player.DadosXadrez.Pontuacao == pontosDeJogadores[j] && usuariosCopia.Exists(jogador => player.Usuario == jogador));
                    Console.WriteLine("|{0,9}|{1,8}|{2,10}|{3,10}|{4,9}|", jogador.Usuario, pontosDeJogadores[j], jogador.DadosXadrez.Vitorias, jogador.DadosXadrez.Derrotas, jogador.DadosXadrez.Empates);
                    usuariosCopia.Remove(jogador.Usuario);

                }
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("");
            }
            if (tipoDeJogo == "naval")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Clear();
                int[] pontosDeJogadores = new int[Jogadores.Count];
                List<string> usuariosCopia = new List<string>();
                for (int i = 0; i < Jogadores.Count; i++)
                {
                    pontosDeJogadores[i] = Jogadores[i].DadosNaval.Pontuacao;
                    usuariosCopia.Add(Jogadores[i].Usuario);
                }
                Array.Sort(pontosDeJogadores);
                Array.Reverse(pontosDeJogadores);
                Console.WriteLine("-----------------------TOP-10-----------------------");
                Console.WriteLine("| Jogador | Pontos | Vitorias | Derrotas | Empates |");

                for (int j = 0; j < 10; j++)
                {
                    //achar o index do player correspondete a primeira pontuação no vetor pontos
                    //Dificil
                    Jogador jogador = Jogadores.Find(player => player.DadosNaval.Pontuacao == pontosDeJogadores[j] && usuariosCopia.Exists(jogador => player.Usuario == jogador));
                    Console.WriteLine("|{0,9}|{1,8}|{2,10}|{3,10}|{4,9}|", jogador.Usuario, pontosDeJogadores[j], jogador.DadosNaval.Vitorias, jogador.DadosNaval.Derrotas, jogador.DadosNaval.Empates);
                    usuariosCopia.Remove(jogador.Usuario);

                }
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("");
            }

        }

        public Jogador FazerLogin(int numDeJogador)
        {
            Console.WriteLine($"Digite o usuario do Jogador {numDeJogador}");
            string player = Console.ReadLine();
            bool jogadorExiste = Jogadores.Exists(jogador => jogador.Usuario == player);
            do
            {
                if (jogadorExiste)
                {
                    Jogador jogador = Jogadores.Find(jogador => jogador.Usuario == player);
                    Console.WriteLine($"Digite o a senha do Jogador {numDeJogador}");
                    string senhaPlayer = Console.ReadLine();

                    while (senhaPlayer != jogador.Senha)
                    {
                        Console.WriteLine("A senha está incorreta, tente novamente");
                        senhaPlayer = Console.ReadLine();
                    }
                    return jogador;

                }
                else
                {
                    Console.WriteLine("Usuario  inexistente, digite um nome de usuario valido");
                    player = Console.ReadLine();
                    jogadorExiste = Jogadores.Exists(jogador => jogador.Usuario == player);
                }
            } while (true);
        }

        public Jogador[] FazerLoginDeJogadores()
        {
            Jogador[] jogadores = new Jogador[2];

            //jogadores[0] = FazerLogin(1);
            //jogadores[1] = FazerLogin(2);

            //Para teste
            jogadores[0] = Jogadores[1];
            jogadores[1] = Jogadores[2];
            Console.Clear();

            return jogadores;


        }
    }
}
