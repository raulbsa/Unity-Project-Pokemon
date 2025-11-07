using Newtonsoft.Json;
// Importa o namespace base do .NET, contém classes fundamentais como Console, Exception, etc.
using System;
using System.Collections;
using System.Linq;
// Permite trabalhar com requisições HTTP, incluindo HttpClient, HttpRequestMessage, HttpResponseMessage.
// Necessário para fazer a requisição à API do Pokémon.
using System.Net.Http;
// Contém tipos para programação assíncrona, incluindo Task e async/await.
// Necessário para usar 'await client.GetStringAsync(...)'.
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using Assets.Scripts;

public class PainelDoPlayer : MonoBehaviour
{
    public TextMeshProUGUI nomeDoPlayer;
    public Image spriteDoPlayer;
    private int IdDoPlayer;
    private int Level;
    private int Base_Stat;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI LevelText;

    public TextMeshProUGUI[] movimentosDoPlayer; // 0,1,2,3
    public Image[] setas;
    public TextMeshProUGUI tipoText;
    public TextMeshProUGUI ppText;

    [HideInInspector] public string[] nomesMovimentos = new string[4];
    [HideInInspector] public string[] tiposMovimentos = new string[4];
    [HideInInspector] public string[] ppsMovimentos = new string[4];
    [HideInInspector] public int ultimoSelecionado = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await CarregarPokemonAsync();
        SelecionarMovimento(0);

        if (IdDoPlayer <= 251)
        {
            int hp = HpGeracao1e2(Level, Base_Stat);
            HP.text = $"{hp}/{hp}";
        } else
        {
            int hp = HpGeracao3ouMais(Level, Base_Stat);
            HP.text = $"{hp}/{hp}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelecionarMovimento(int index)
    {
        if (index < 0 || index >= nomesMovimentos.Length) return;

        ultimoSelecionado = index;

        tipoText.text = tiposMovimentos[index];
        ppText.text = ppsMovimentos[index];

        for (int i = 0; i < setas.Length; i++)
            setas[i].gameObject.SetActive(i == index);
    }

    public int HpGeracao1e2(int level, int base_stat)
    {
        int hp = (((base_stat * 2) * level) / 100) + level + 10;
        return hp;
    }

    public int HpGeracao3ouMais(int level, int base_stat)
    {
        int hp = (((2 * base_stat * level) / 100) + level + 10);
        return hp;
    }

    private async Task CarregarPokemonAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Número aleatório entre 1 e 1328 (total de pokemons)
                int indexPokemon = UnityEngine.Random.Range(1, 1026); // int do ID do player
                IdDoPlayer = indexPokemon;
                int indexTeste = 1000;
                Debug.Log(indexPokemon);

                string resposta = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{indexPokemon}");
                Pokemon p = JsonConvert.DeserializeObject<Pokemon>(resposta);
                nomeDoPlayer.text = p.name.ToUpper();
                StartCoroutine(BaixarImagem(p.sprites.back_default));

                int orderDoPlayer = p.order; // Level
                Level = orderDoPlayer;
                LevelText.text = orderDoPlayer.ToString();

                string base_statDoPlayer = p.stats[0].base_stat;
                int base_statDoPlayerInt = int.Parse(base_statDoPlayer);
                Base_Stat = base_statDoPlayerInt;

                for (int i = 0; i < 4 && i < p.moves.Count; i++)
                {
                    nomesMovimentos[i] = p.moves[i].move.name;
                    movimentosDoPlayer[i].text = nomesMovimentos[i].ToUpper();

                    string urlMovimento = p.moves[i].move.url;
                    string respostaMove = await client.GetStringAsync(urlMovimento);
                    MoveDetails detalhes = JsonConvert.DeserializeObject<MoveDetails>(respostaMove);

                    tiposMovimentos[i] = detalhes.type.name.ToUpper();
                    ppsMovimentos[i] = $"{detalhes.pp}/{detalhes.pp}";
                }

            }
            catch (Exception ex)
            {
                Debug.Log($"Temos um problema: {ex.Message}");
            }
        }
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