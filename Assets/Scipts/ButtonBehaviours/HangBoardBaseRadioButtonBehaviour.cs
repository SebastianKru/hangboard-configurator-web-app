using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioButtonHangBoardBase : MonoBehaviour
{
    // the UI Toggle component of this GameObject
    private Toggle toggle;
    // one of the 3 Base sizes 
    public GameObject hangboardBase;
    // a Parent Object to which all Bases will be spawned
    public Transform anchor;

    public void Start()
    {
        //Fetch the Toggle GameObject
        toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });

        // only one toggle of the ToggleGroup can be active at the same time
        // find the active Toggle and spawn the hangboardBase 
        if (toggle.isOn)
        {
            GameObject.Instantiate(hangboardBase, anchor);
        }
    }

    // If a user presses a Toggle, the corresponding hangboardBase will be instantiated
    // any other existing hangboardBase will be deleted 
    void ToggleValueChanged(Toggle change)
    {
       if(toggle.isOn)
       {
            GameObject.Instantiate(hangboardBase, anchor);
            if(anchor.childCount>1)
            {
                Destroy(anchor.GetChild(0).gameObject);
            }
       }
       else
       {
            if(hangboardBase.activeSelf)
            {
                Destroy(anchor.GetChild(0).gameObject);
            }
       }
    }
}

