

public class PauseWindow : BaseWindow
{
    public void ExitMainMenu()
    {
        GameManager.Instance.LoadScene(GameScene.MAIN_MENU);
    }

    public void TogglePause() 
    {
        ToggleWindow();
        GameManager.Instance.TogglePause();
    }
    
    public void Awake()
    {
        HideWindow();
    }


}
