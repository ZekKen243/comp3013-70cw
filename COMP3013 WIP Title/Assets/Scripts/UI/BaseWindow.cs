using System;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
  public virtual void OnShow() {}
  public virtual void OnHide() {}
  

  public void ShowWindow()
  {
    gameObject.SetActive(true);
    OnShow();
  }

  public void HideWindow()
  {
    gameObject.SetActive(false);
    OnHide();
  }

  public void ToggleWindow()
  {
    if(IswindowVisible())
    {
      HideWindow();
      return;
    }

    ShowWindow();
  }

  public bool IswindowVisible()
  {
    return gameObject.activeSelf;
  }
}