using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButtonBehaviour : MonoBehaviour
{
    // holds all the Info texts
    public GameObject info;

    // whenever a user clicks anywhere on the WebGL window we want to close the infos
    // unless the User clicks on an Info itself
    void Update()
    {

        if(Input.GetMouseButtonDown(0) &&
            //UI Elements of the type Info are labeled in UI layer 8
            !PointerOverUIElement.IsPointerOverUIElement(8)
            )
        {
            info.SetActive(false);
        }
    }

    // On Button Click toggle between active and inactive 
    public void OnInfoButtonClicked()
    {
        if(info.activeSelf)
        {
            info.SetActive(false);
        }
        else
        {
            info.SetActive(true); 
        }
    }
}
