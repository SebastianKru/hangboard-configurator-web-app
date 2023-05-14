using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MouseOverHoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{

    Image [] childimages;
    TMP_Text[] texts;
    float defaultAlpha = 0.7f;
    float highlightAlpha = 0.9f;

    public void Start()
    {
        childimages = this.GetComponentsInChildren<Image>();
        texts = this.GetComponentsInChildren<TMP_Text>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeAlpha(defaultAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeAlpha(1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeAlpha(highlightAlpha);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ChangeAlpha(1f);
    }

    private void ChangeAlpha(float newAlpha)
    {
        foreach (Image img in childimages)
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
