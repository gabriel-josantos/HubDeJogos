using Hub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Hub.Service;

namespace Hub.Utils;

public class Helpers
{
    public static void DeserializarJson(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        else
        {
            string jsonString = File.ReadAllText(filePath);
            GameHub.Jogadores = JsonSerializer.Deserialize<List<Jogador?>>(jsonString);
        }

    }
    public static void SerializarJson(string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        string jsonString = JsonSerializer.Serialize(GameHub.Jogadores, options);
        File.WriteAllText(filePath, jsonString);
    }
    public static int TrocarJogador(int vezDoJogador)
    {
        vezDoJogador = vezDoJogador == 1 ? 2 : 1;
        return vezDoJogador;
    }


}

