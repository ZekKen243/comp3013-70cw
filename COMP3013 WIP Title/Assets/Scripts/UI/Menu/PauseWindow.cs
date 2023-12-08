

public class PauseWindow : BaseWindow
{
    public void ExitMainMenu()
    {
        GameManager.Instance.LoadScene(GameScene.MAIN_MENU);
    }

    public void TogglePause()
    {
        if(GameManager.Instance.isGamePaused)
        {
            ResumeGame();
            return;
        }

        PauseGame();
    }

    public void ResumeGame() 
    {
        HideWindow();
        GameManager.Instance.SetPause(false);
    }

    public void PauseGame() 
    {
        ShowWindow();
        GameManager.Instance.SetPause(true);
    }

    public void Awake()
    {
        HideWindow();
    }


}
