using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MouseOverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{

    Image [] images;
    TMP_Text[] texts;
    float dimmedAlpha = 0.7f;
    float highlightAlpha = 0.9f;

    public void Start()
    {
        images = this.GetComponentsInChildren<Image>();
        texts = this.GetComponentsInChildren<TMP_Text>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeHoverColor(dimmedAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeHoverColor(1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeHoverColor(highlightAlpha);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ChangeHoverColor(1f);
    }

    private void ChangeHoverColor(float newAlpha)
    {
        foreach (Image img in images)
        {
            Color tmp = img.color;
            tmp.a = newAlpha;
            img.color = tmp;
        }

        foreach(TMP_Text txt in texts)
        {
            Color tmp = txt.color;
            tmp.a = newAlpha;
            txt.color = tmp; 
        }
    }
}
