//using Assets.Scripts;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEditor.PackageManager;

//namespace Assets.Scripts
//{
//    public class InfosDaApi
//    {
//        using (HttpClient client = new HttpClient())
//        {
//            try
//            {
//                // Número aleatório entre 1 e 1328 (total de pokemons)
//                int indexPokemon = UnityEngine.Random.Range(1, 1026); // int do ID do player
//            int indexTeste = 1000;
//            Debug.Log(indexPokemon);

//                string resposta = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{indexTeste}");
//            Pokemon p = JsonConvert.DeserializeObject<Pokemon>(resposta);
//            //Pokemon p = JsonSerializer.Deserialize<Pokemon>(resposta)!;
//            // Para acessar o nome do pokemon
//            nome = p.name; // string do nome do player
//                Debug.Log(nome);
//                nomeDoPlayer.text = nome; // Nome do pokemon em text
//                Debug.Log(nomeDoPlayer.text);

//                ///////////////////////////////////////////////////////////////////////////////////////////////
//                // Para acessar o tipo do Pokemon (Não será necessário para o projeto)
//                foreach (var t in p.types)
//                {
//                    tipo = t.type.name; // string do tipo do player
//                    Debug.Log(tipo);
//                }

//        ///////////////////////////////////////////////////////////////////////////////////////////////
//        // Para acessar a sprite do player
//        spritePlayer = p.sprites.back_default; // string da sprite do player
//        Debug.Log(spritePlayer);
//        StartCoroutine(BaixarImagem(spritePlayer));

//        ///////////////////////////////////////////////////////////////////////////////////////////////
//        // Para pegar apenas os primeiros 4 movimentos do pokemon se houver
//        var primeiros4 = p.moves.Take(4).ToList();
//        // Primeiro movimento
//        if (p.moves[0].move.name != null)
//        {
//            movimento1 = p.moves[0].move.name; // string do movimento
//            movimento1DoPlayer.text = movimento1; // Movimento em tipo text
//            Debug.Log($"Primeiro movimento: {movimento1}");
//        }

//        // Segundo movimento
//        if (p.moves[1].move.name != null)
//        {
//            movimento2 = p.moves[1].move.name; // string do movimento
//            movimento2DoPlayer.text = movimento2; // Movimento em tipo text
//            Debug.Log($"Segundo movimento: {movimento2}");
//        }

//        // Terceiro movimento
//        if (p.moves[2].move.name != null)
//        {
//            movimento3 = p.moves[2].move.name; // string do movimento
//            movimento3DoPlayer.text = movimento3; // Movimento em tipo text
//            Debug.Log($"Terceiro movimento: {movimento3}");
//        }

//        // Quarto movimento
//        if (p.moves[3].move.name != null)
//        {
//            movimento4 = p.moves[3].move.name; // string do movimento
//            movimento4DoPlayer.text = movimento4; // Movimento em tipo text
//            Debug.Log($"Quarto movimento: {movimento4}");
//        }

//        ///////////////////////////////////////////////////////////////////////////////////////////////
//        // Acessando o tipo e o pp do primeiro movimento
//        urlDoPrimeiroMovimento = p.moves[0].move.url;
//        Debug.Log($"Tipo da url: {urlDoPrimeiroMovimento.GetType()}");
//        string respostaDoTipo = await client.GetStringAsync(urlDoPrimeiroMovimento);
//        MoveDetails MovimentoDetalhes = JsonConvert.DeserializeObject<MoveDetails>(respostaDoTipo);

//        // Pegando o tipo do primeiro movimento
//        tipoDoPrimeiroMovimento = MovimentoDetalhes.type.name; // string do tipo do player
//        tipoDoPrimeiroMovimentoText.text = tipoDoPrimeiroMovimento; // tipo do player em text
//        Debug.Log(tipoDoPrimeiroMovimento);

//        // Pegando o pp do primeiro movimento
//        pp = MovimentoDetalhes.pp; // string do pp do player
//        ppText.text = $"{pp}/{pp}"; // pp do player em text
//        Debug.Log(pp);

//            }
//            catch (Exception ex)
//            {
//                Debug.Log($"Temos um problema: {ex.Message}");
//            }
//        }
//    }
//}
