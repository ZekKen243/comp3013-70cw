using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Mountains - Area One");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
