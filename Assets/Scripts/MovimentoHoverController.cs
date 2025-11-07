using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MovimentoHoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int indexDoMovimento; // 0, 1, 2 ou 3
    public PainelDoPlayer painel; // referência ao script principal

    // Quando o mouse entra no texto do movimento
    public void OnPointerEnter(PointerEventData eventData)
    {
        SelecionarMovimento(indexDoMovimento);
    }

    // Quando o mouse sai do texto
    public void OnPointerExit(PointerEventData eventData)
    {
        // mantém o último movimento selecionado
        painel.SelecionarMovimento(painel.ultimoSelecionado);
    }
    private void SelecionarMovimento(int index)
    {
        painel.ultimoSelecionado = index;

        for (int i = 0; i < painel.setas.Length; i++)
            painel.setas[i].gameObject.SetActive(i == index);

        painel.tipoText.text = painel.tiposMovimentos[index];
        painel.ppText.text = painel.ppsMovimentos[index];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
