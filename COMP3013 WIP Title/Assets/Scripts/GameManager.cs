
using UnityEngine;
using UnityEngine.SceneManagement;


class GameManager: MonoBehaviour
{
  public bool isGamePaused = false;
  private static GameManager instance = null;

  public static GameManager Instance
  {
    get
    {
      if (instance == null)
      {
        GameObject go = new();
        instance = go.AddComponent<GameManager>();
      }

      return instance;
    }
  }

  private void Awake()
  {
    CheckInstanceState();
    Initialise();
  }

  private void CheckInstanceState()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
      return;
    }

    Destroy(gameObject);
  }

  private void Initialise()
  {
    CardProtoManager.Instance.Initialise();
  }

  public void TogglePause()
  {
    SetPause(!isGamePaused);
  }

  public void SetPause(bool pause)
  {
    isGamePaused = pause;
    Time.timeScale = isGamePaused ? 0.0f: 1.0f;

  }

  public void LoadScene(GameScene gameScene)
  {
    SceneManager.LoadScene(gameScene.ToString());
  }




}



