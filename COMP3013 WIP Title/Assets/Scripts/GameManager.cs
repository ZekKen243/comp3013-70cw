
using UnityEngine;
using UnityEngine.SceneManagement;


class GameManager: MonoBehaviour
{

  public void TogglePause()
  {
    SetPause(!isGamePaused);
  }

  public void SetPause(bool pause)
  {
    isGamePaused = pause;

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

  
  public bool isGamePaused = false;
  private static GameManager instance = null;
}



