
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameScene 
{
  MAIN_MENU,
  MAIN_SCENE,
  SAMPLE_SCENE
}

class GameManager: MonoBehaviour
{
  private static GameManager instance = null;
  
  public bool isGamePaused = false;

  public void TogglePause()
  {
    isGamePaused = !isGamePaused;

    if(isGamePaused)
    {
      Time.timeScale = 0.0f;
      return;
    }

    Time.timeScale = 1.0f;
  }

  public void LoadScene(GameScene gameScene)
  {
    SceneManager.LoadScene(gameScene.ToString());
  }

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }
  }

  public static GameManager Instance
  {
    get
    {
      if (instance == null)
      {
        GameObject go = new GameObject();
        instance = go.AddComponent<GameManager>();
      }

      return instance;
    }
  }

}



