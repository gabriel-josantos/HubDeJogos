
using Hub.Model.xadrez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Hub.Utils;
using Hub.Model;

namespace Hub.Service
{
    public class GameHub
    {
        static public List<Jogador> Jogadores { get; set; }

        public GameHub()
        {
            Jogadores = new List<Jogador>();
        }

        public void CadastrarNovoJogador(string filePath)
        {
            Console.WriteLine("Insira o nome de usuario");
            string usuario = Console.ReadLine();

            while (ChecarExistenciaDeJogador(usuario))
            {
                Console.WriteLine("Este nome de usuario ja esta sendo utilizado, por favor escolha outro");
                usuario = Console.ReadLine();
            }

            Console.WriteLine("Digite uma senha (min 8 chars)");
            string senha = Console.ReadLine();
            while (senha.Length < 8)
            {
                Console.WriteLine("A senha deve possuir no minimo 8 caracteres, digite novamente");
                senha = Console.ReadLine();
            }

            Jogador jogador = new Jogador(usuario, senha);

            Jogadores.Add(jogador);
            Console.WriteLine($"usuario {usuario} criado com sucesso");

            Helpers.SerializarJson(filePath);
        }

        public void MostrarJogadoresCadastrados()
        {

            Jogadores.ForEach(player => Console.WriteLine($"Usuario : {player.Usuario}"));

        }

        public void AtualizarDadosDeJogador(string filePath)
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
                            Helpers.SerializarJson(filePath);
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
                    Console.WriteLine("Digite a nova senha");
                    string novaSenha = Console.ReadLine();
                    jogador.Senha = novaSenha;
                    Helpers.SerializarJson(filePath);
                    Console.WriteLine("Senha atualizada com sucesso!");
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

        public void DeletarJogador(string filePath)
        {
            Console.WriteLine("Digite o usuario do jogador que deseja deletar");
            string jogadorParaDeletar = Console.ReadLine();
            if (ChecarExistenciaDeJogador(jogadorParaDeletar))
            {
                Jogador jogador = EncontrarJogador(jogadorParaDeletar);
                Jogadores.Remove(jogador);
                Helpers.SerializarJson(filePath);
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

        public void AtribuirResultadoDeJogo(Jogador jogador1, Jogador jogador2, int vencedor, string filePath, string tipoDeJgo)
        {
            if (tipoDeJgo == "xadrez")
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
                Helpers.SerializarJson(filePath);
            }
            if (tipoDeJgo == "velha")
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
                Helpers.SerializarJson(filePath); ;
            }

            if (tipoDeJgo == "naval")
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
                Helpers.SerializarJson(filePath);
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
            Console.Write($"Digite o usuario do Jogador {numDeJogador}: ");
            string player = Console.ReadLine();
            bool jogadorExiste = Jogadores.Exists(jogador => jogador.Usuario == player);
            do
            {
                if (jogadorExiste)
                {
                    Jogador jogador = Jogadores.Find(jogador => jogador.Usuario == player);
                    Console.Write($"Digite o a senha do Jogador {numDeJogador}: ");
                    string senhaPlayer = Console.ReadLine();

                    while (senhaPlayer != jogador.Senha)
                    {
                        Console.Write("A senha está incorreta, tente novamente: ");
                        senhaPlayer = Console.ReadLine();
                    }
                    return jogador;

                }
                else
                {
                    Console.Write("Usuario  inexistente, digite um nome de usuario valido: ");
                    player = Console.ReadLine();
                    jogadorExiste = Jogadores.Exists(jogador => jogador.Usuario == player);
                }
            } while (true);
        }

        public Jogador[] FazerLoginDeJogadores()
        {
            Jogador[] jogadores = new Jogador[2];

            jogadores[0] = FazerLogin(1);
            Console.WriteLine("");
            jogadores[1] = FazerLogin(2);

            //Para teste
            //jogadores[0] = Jogadores[1];
            //jogadores[1] = Jogadores[2];
            Console.Clear();

            return jogadores;


        }

        public void ResetarPontuações(string filePath)
        {
            foreach (Jogador jogador in Jogadores)
            {

                jogador.DadosVelha.Vitorias = 0;
                jogador.DadosVelha.Derrotas = 0;
                jogador.DadosVelha.Empates = 0;
                jogador.DadosVelha.ObterPontuacao(0, 0, 0);

                jogador.DadosXadrez.Vitorias = 0;
                jogador.DadosXadrez.Derrotas = 0;
                jogador.DadosXadrez.Empates = 0;
                jogador.DadosXadrez.ObterPontuacao(0, 0, 0);

                jogador.DadosNaval.Vitorias = 0;
                jogador.DadosNaval.Derrotas = 0;
                jogador.DadosNaval.Empates = 0;
                jogador.DadosNaval.ObterPontuacao(0, 0, 0);

            }

            string writeJsonString = JsonSerializer.Serialize(Jogadores);
            File.WriteAllText(filePath, writeJsonString);
            Console.WriteLine("Pontuações resetadas com sucesso!");
        }

    }
}
