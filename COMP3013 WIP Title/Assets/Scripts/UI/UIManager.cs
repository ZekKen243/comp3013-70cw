using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InventoryWindow inventoryWindow = null;
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
        inventoryWindow = GetComponentInChildren<InventoryWindow>(true);
        pauseWindow = GetComponentInChildren<PauseWindow>(true);
    }
    
    public void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventoryWindow)
        {
            inventoryWindow.ToggleWindow();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pauseWindow)
        {
            pauseWindow.TogglePause();
        }
    }


}
