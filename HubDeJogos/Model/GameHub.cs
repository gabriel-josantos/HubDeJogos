
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
        static public List<Jogador> Jogadores {get;protected set;}

        public GameHub()
        {
            Jogadores = new List<Jogador>();
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
                Console.WriteLine("Insira seu nome");
                string nome = Console.ReadLine();
                Jogador jogador = new Jogador(usuario, nome);
                Jogadores.Add(jogador);
                Console.WriteLine($"usuario {usuario} criado com sucesso");
            }

            string jsonString = JsonSerializer.Serialize(Jogadores);
            File.WriteAllText(fileName, jsonString);
        }

        public void MostrarJogadoresCadastrados()
        {

            Jogadores.ForEach(player => Console.WriteLine($"Usuario : {player.Usuario} | Nome: {player.Nome}"));

        }

        public void AtualizarDadosDeJogador(string fileName)
        {
            Console.WriteLine("Digite o usuario do jogador que deseja atualizar");
            string jogadorParaAtualizar = Console.ReadLine();
            if (ChecarExistenciaDeJogador(jogadorParaAtualizar))
            {
                Jogador jogador = EncontrarJogador(jogadorParaAtualizar);
                Console.WriteLine("Digite 1 para atualizar o usuario");
                Console.WriteLine("Digite 2 para atualizar o nome");
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
                            string writeJsonString = JsonSerializer.Serialize(Jogadores);
                            File.WriteAllText(fileName, writeJsonString);
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
                    string novoNome = Console.ReadLine();
                    jogador.Nome = novoNome;
                    string writeJsonString = JsonSerializer.Serialize(Jogadores);
                    File.WriteAllText(fileName, writeJsonString);
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
                string writeJsonString = JsonSerializer.Serialize(Jogadores);
                File.WriteAllText(fileName, writeJsonString);
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



    }
}
