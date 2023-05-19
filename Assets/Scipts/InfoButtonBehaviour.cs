using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButtonBehaviour : MonoBehaviour
{
    public GameObject info;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !PointerOverUIElement.IsPointerOverUIElement(8))
        {
            info.SetActive(false);
        }
    }

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
