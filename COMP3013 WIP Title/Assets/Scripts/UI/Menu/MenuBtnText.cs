
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuBtnText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color HoverColor = Color.white;
    public Color NormalColor = Color.white;
    public TextMeshProUGUI text = null;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = NormalColor;
    }
}
