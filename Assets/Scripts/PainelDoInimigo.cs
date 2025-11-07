using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using Assets.Scripts;

public class PainelDoInimigo : MonoBehaviour
{
    private string nome;
    private string tipo;
    private string spritePlayer;
    public TextMeshProUGUI nomeDoInimigo;
    public Image spriteDoInimigo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await CarregarPokemonInimigoAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async Task CarregarPokemonInimigoAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                int indexPokemon = UnityEngine.Random.Range(1, 1026); // int do ID do inimigo
                int indexTeste = 25; // pikachu = 25
                Debug.Log(indexPokemon);

                string resposta = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{indexTeste}");
                Pokemon p = JsonConvert.DeserializeObject<Pokemon>(resposta);
                nome = p.name; // string do nome do inimigo
                Debug.Log(nome);
                nomeDoInimigo.text = nome.ToUpper(); // Nome do pokemon em text
                Debug.Log(nomeDoInimigo.text);

                ///////////////////////////////////////////////////////////////////////////////////////////////
                // Para acessar a sprite do inimigo
                spritePlayer = p.sprites.front_default; // string da sprite do inimigo
                Debug.Log(spritePlayer);
                StartCoroutine(BaixarImagem(spritePlayer));

            }
            catch (Exception ex)
            {
                Debug.Log($"Temos um problema: {ex.Message}");
            }
        }
    }

    IEnumerator BaixarImagem(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D textura = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(textura, new Rect(0, 0, textura.width, textura.height), new Vector2(0.5f, 0.5f));
            spriteDoInimigo.sprite = sprite;
        }
        else
        {
            Debug.LogError("Erro ao baixar imagem: " + request.error);
        }

    }
}