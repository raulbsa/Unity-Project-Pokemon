// Fornece classes para serialização e desserialização de JSON (JsonSerializer, JsonDocument, etc.).
// OBS: no Unity padrão, esse namespace não funciona, causa erro "namespace 'Json' não existe".
// Para Unity, talvez usar Newtonsoft.Json ou JsonUtility.
//using System.Text.Json;
using Newtonsoft.Json;
// Importa o namespace base do .NET, contém classes fundamentais como Console, Exception, etc.
using System;
using System.Collections;
// Permite usar coleções genéricas, como List<T>, Dictionary<TKey, TValue>, etc.
// No meu caso, é necessário para armazenar a lista de tipos do Pokémon.
using System.Collections.Generic;
using System.Linq;

// Permite trabalhar com requisições HTTP, incluindo HttpClient, HttpRequestMessage, HttpResponseMessage.
// Necessário para fazer a requisição à API do Pokémon.
using System.Net.Http;
// Contém tipos para programação assíncrona, incluindo Task e async/await.
// Necessário para usar 'await client.GetStringAsync(...)'.
using System.Threading.Tasks;
// Permite usar membros estáticos da classe JSType diretamente, sem precisar escrever JSType.Membro.
// No seu código de API Pokémon, **essa linha não é necessária** e provavelmente pode ser removida.
//using static System.Runtime.InteropServices.JavaScript.JSType;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;
//using UnityEngine.UIElements;

public class PainelDoPlayer : MonoBehaviour
{
    private string nome;
    private string tipo;
    private string spritePlayer;
    public TextMeshProUGUI nomeDoPlayer;
    public Image spriteDoPlayer;

    private string movimento1;
    private string movimento2;
    private string movimento3;
    private string movimento4;
    public TextMeshProUGUI movimento1DoPlayer;
    public TextMeshProUGUI movimento2DoPlayer;
    public TextMeshProUGUI movimento3DoPlayer;
    public TextMeshProUGUI movimento4DoPlayer;

    private string urlDoPrimeiroMovimento;
    private string tipoDoPrimeiroMovimento;
    public TextMeshProUGUI tipoDoPrimeiroMovimentoText;
    private string pp;
    public TextMeshProUGUI ppText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        

        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Número aleatório entre 1 e 1328 (total de pokemons)
                int indexPokemon = UnityEngine.Random.Range(1, 1026); // int do ID do player
                int indexTeste = 500;
                Debug.Log(indexPokemon);

                string resposta = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{indexTeste}");
                Pokemon p = JsonConvert.DeserializeObject<Pokemon>(resposta);
                //Pokemon p = JsonSerializer.Deserialize<Pokemon>(resposta)!;
                // Para acessar o nome do pokemon
                nome = p.name; // string do nome do player
                Debug.Log(nome);
                nomeDoPlayer.text = nome; // Nome do pokemon em text
                Debug.Log(nomeDoPlayer.text);

                ///////////////////////////////////////////////////////////////////////////////////////////////
                // Para acessar o tipo do Pokemon (Não será necessário para o projeto)
                foreach (var t in p.types)
                {
                    tipo = t.type.name; // string do tipo do player
                    Debug.Log(tipo);
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////
                // Para acessar a sprite do player
                spritePlayer = p.sprites.back_default; // string da sprite do player
                Debug.Log(spritePlayer);
                StartCoroutine(BaixarImagem(spritePlayer));

                ///////////////////////////////////////////////////////////////////////////////////////////////
                // Para pegar apenas os primeiros 4 movimentos do pokemon se houver
                var primeiros4 = p.moves.Take(4).ToList();
                // Primeiro movimento
                if (p.moves[0].move.name != null)
                {
                    movimento1 = p.moves[0].move.name; // string do movimento
                    movimento1DoPlayer.text = movimento1; // Movimento em tipo text
                    Debug.Log($"Primeiro movimento: {movimento1}");
                }

                // Segundo movimento
                if (p.moves[1].move.name != null)
                {
                    movimento2 = p.moves[1].move.name; // string do movimento
                    movimento2DoPlayer.text = movimento2; // Movimento em tipo text
                    Debug.Log($"Segundo movimento: {movimento2}");
                }
                
                // Terceiro movimento
                if (p.moves[2].move.name != null)
                {
                    movimento3 = p.moves[2].move.name; // string do movimento
                    movimento3DoPlayer.text = movimento3; // Movimento em tipo text
                    Debug.Log($"Terceiro movimento: {movimento3}");
                }
                
                // Quarto movimento
                if (p.moves[3].move.name != null)
                {
                    movimento4 = p.moves[3].move.name; // string do movimento
                    movimento4DoPlayer.text = movimento4; // Movimento em tipo text
                    Debug.Log($"Quarto movimento: {movimento4}");
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////
                // Acessando o tipo e o pp do primeiro movimento
                urlDoPrimeiroMovimento = p.moves[0].move.url;
                Debug.Log($"Tipo da url: {urlDoPrimeiroMovimento.GetType()}");
                string respostaDoTipo = await client.GetStringAsync(urlDoPrimeiroMovimento);
                MoveDetails MovimentoDetalhes = JsonConvert.DeserializeObject<MoveDetails>(respostaDoTipo);

                // Pegando o tipo do primeiro movimento
                tipoDoPrimeiroMovimento = MovimentoDetalhes.type.name; // string do tipo do player
                tipoDoPrimeiroMovimentoText.text = tipoDoPrimeiroMovimento; // tipo do player em text
                Debug.Log(tipoDoPrimeiroMovimento);

                // Pegando o pp do primeiro movimento
                pp = MovimentoDetalhes.pp; // string do pp do player
                ppText.text = pp; // pp do player em text
                Debug.Log(pp);

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

    // Declaração de uma coroutine chamada BaixarImagem, que recebe uma string com a URL da imagem.
    // Coroutines permitem executar tarefas assíncronas ao longo do tempo (como downloads).
    IEnumerator BaixarImagem(string url)
    {
        // Cria uma requisição HTTP para baixar uma textura da URL fornecida.
        // UnityWebRequestTexture.GetTexture é usado especificamente para imagens.
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        // Envia a requisição e espera até que o download seja concluído.
        // O yield return pausa a execução da coroutine até que a resposta chegue.
        yield return request.SendWebRequest();
        
        // Verifica se o download foi bem-sucedido. Se sim, continua com o processamento da imagem.
        if (request.result == UnityWebRequest.Result.Success)
        {
            // Extrai a textura da resposta da requisição. Essa textura representa a imagem que foi baixada.
            Texture2D textura = DownloadHandlerTexture.GetContent(request);
            //textura.filterMode = FilterMode.Point;
            //textura.Apply();
            // Cria um Sprite a partir da textura baixada.
            // Rect(0, 0, largura, altura) define a área da textura usada.
            // Vector2(0.5f, 0.5f) define o ponto central do sprite(pivot).
            Sprite sprite = Sprite.Create(textura, new Rect(0, 0, textura.width, textura.height), new Vector2(0.5f, 0.5f));
            // Substitui o sprite atual do componente Image da UI pelo novo sprite criado a partir da imagem baixada.
            spriteDoPlayer.sprite = sprite;
        }
        else
        {
            Debug.LogError("Erro ao baixar imagem: " + request.error);
        }

    }

}

[System.Serializable]
public class Pokemon
{
    public string name { get; set; }
    public List<TypeSlot> types { get; set; }
    public Sprites sprites { get; set; }
    public List<MoveSlot> moves { get; set; }
}

// Sprites
public class Sprites
{
    public string front_default { get; set; }
    public string back_default { get; set; }
}

// Tipo do Player
public class TypeSlot
{
    public TypeInfo type { get; set; }
}

// Nome do tipo do player
public class TypeInfo
{
    public string name { get; set; }
}

// Abilidades do player
public class MoveSlot
{
    public MoveInfo move { get; set; }
}

public class MoveInfo
{
    public string name {  set; get; }
    public string url { set; get; }
}

// Tipo e pp do primeiro movimento do player
public class MoveDetails
{
    public string name { get; set; }
    public MoveType type { get; set; }
    public string pp { get; set; }
}

public class MoveType
{
    public string name { get; set; }
    public string url { get; set; }
}
