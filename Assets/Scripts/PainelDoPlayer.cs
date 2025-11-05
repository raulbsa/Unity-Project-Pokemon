using UnityEngine;
// Importa o namespace base do .NET, contém classes fundamentais como Console, Exception, etc.
using System;
// Permite usar coleções genéricas, como List<T>, Dictionary<TKey, TValue>, etc.
// No meu caso, é necessário para armazenar a lista de tipos do Pokémon.
using System.Collections.Generic;
// Permite trabalhar com requisições HTTP, incluindo HttpClient, HttpRequestMessage, HttpResponseMessage.
// Necessário para fazer a requisição à API do Pokémon.
using System.Net.Http;
// Fornece classes para serialização e desserialização de JSON (JsonSerializer, JsonDocument, etc.).
// OBS: no Unity padrão, esse namespace não funciona, causa erro "namespace 'Json' não existe".
// Para Unity, talvez usar Newtonsoft.Json ou JsonUtility.
//using System.Text.Json;
using Newtonsoft.Json;
// Contém tipos para programação assíncrona, incluindo Task e async/await.
// Necessário para usar 'await client.GetStringAsync(...)'.
using System.Threading.Tasks;
// Permite usar membros estáticos da classe JSType diretamente, sem precisar escrever JSType.Membro.
// No seu código de API Pokémon, **essa linha não é necessária** e provavelmente pode ser removida.
//using static System.Runtime.InteropServices.JavaScript.JSType;



public class PainelDoPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        string nome;
        string tipo;
        string spritePlayer;

        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Número aleatório entre 1 e 1328 (total de pokemons)
                int indexPokemon = UnityEngine.Random.Range(1, 1329);
                Debug.Log(indexPokemon);

                string resposta = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{indexPokemon}");
                Pokemon p = JsonConvert.DeserializeObject<Pokemon>(resposta);
                //Pokemon p = JsonSerializer.Deserialize<Pokemon>(resposta)!;
                // Para acessar o nome do pokemon
                nome = p.name;
                Debug.Log(nome);


                // Para acessar o tipo do Pokemon
                foreach (var t in p.types)
                {
                    tipo = t.type.name;
                    Debug.Log(tipo);
                }

                // Para acessar a sprite do player
                spritePlayer = p.sprites.back_default;
                Debug.Log(spritePlayer);

            }
            catch (Exception ex)
            {
                Debug.Log($"Temos um problema: {ex.Message}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Pokemon
{
    public string name { get; set; }
    public List<TypeSlot> types { get; set; }
    public Sprites sprites { get; set; }
}

public class Sprites
{
    public string front_default { get; set; }
    public string back_default { get; set; }
}
public class TypeSlot
{
    public TypeInfo type { get; set; }
}

public class TypeInfo
{
    public string name { get; set; }
}