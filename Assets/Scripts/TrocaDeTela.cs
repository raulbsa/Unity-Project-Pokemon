using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrocaDeTela : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelTelaInicial;
    [SerializeField] private GameObject painelTelaPrincipal;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void PainelInicial()
    {
        StartCoroutine(ExecutarComDelay());
        painelTelaInicial.SetActive(false);
        painelTelaPrincipal.SetActive(true);
    }

    public void SairDoJogo()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }


    IEnumerator ExecutarComDelay()
    {
        Debug.Log("Ação 1: Começou");

        // Espera 2 segundos
        yield return new WaitForSeconds(5f);

        Debug.Log("Ação 2: Depois de 2 segundos");
    }

}
