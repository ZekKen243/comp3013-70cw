
using System;
using UnityEngine;
using UnityEngine.UI;
public class CardUsageBar : MonoBehaviour
{

    public float progress
    {
        get
        {
            return iconImage ? iconImage.fillAmount: 0.0f;
        }

        set
        {
            if(iconImage == null)
            {
                return;
            }

            iconImage.fillAmount = value;
        }
    }

    private Image iconImage = null;

    void Awake()
    {
        iconImage = GetComponent<Image>();
        progress = 0f;
    }

    

}
