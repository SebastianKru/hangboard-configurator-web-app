using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class <c>RadioButtonsHoverHighlight</c>
/// changes the brightness of a radioButton if the Mouse is hovering over it
/// </summary>
public class RadioButtonsHoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    // every radioButton consists of multiple Images
    Image [] childimages;
    // every radioButton consists of multiple text fields
    TMP_Text[] texts;

    // the script changes the alpha component of the colour 
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


    // iterate over the child images and the texts
    // change the alpha value to newAlpha 
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
