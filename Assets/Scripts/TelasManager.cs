using UnityEngine;
using UnityEngine.SceneManagement;


public class TelasManager : MonoBehaviour
{
    [SerializeField] private GameObject PreviousScene;
    [SerializeField] private GameObject CurrentScene;
    [SerializeField] private GameObject NextScene;
    public void Next()
    {
        //SceneManager.LoadScene();
        NextScene.SetActive(true);
        CurrentScene.SetActive(false);
        PreviousScene.SetActive(false);
    }

    public void Previous()
    {
        PreviousScene.SetActive(true);
        CurrentScene.SetActive(false);
        NextScene.SetActive(false);
    }
}
