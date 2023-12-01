using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
  public void ShowWindow()
  {
    gameObject.SetActive(true);
  }

  public void HideWindow()
  {
    gameObject.SetActive(false);
  }

  public void ToggleWindow()
  {
    gameObject.SetActive(!gameObject.activeSelf);
  }

  public bool IswindowVisible()
  {
    return gameObject.activeSelf;
  }
}