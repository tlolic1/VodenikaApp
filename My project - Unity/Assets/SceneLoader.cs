using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadARScene()
    {
        SceneManager.LoadScene("Scena moja");
    }

    public void LoadQuiz()
    {
        SceneManager.LoadScene("Quiz");
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
