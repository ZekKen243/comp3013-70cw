using UnityEngine;

public class UIManager : MonoBehaviour
{
    private PauseWindow pauseWindow = null;

    public void Awake()
    {
        InitRefferences();

        if(pauseWindow)
        {
            pauseWindow.HideWindow();
        }
    }

    void InitRefferences()
    {
        pauseWindow = GetComponentInChildren<PauseWindow>(true);
    }
    
    public void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseWindow)
        {
            pauseWindow.TogglePause();
        }
    }


}
