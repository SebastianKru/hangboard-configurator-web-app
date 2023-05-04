using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioButtonBehaviour : MonoBehaviour
{
    Toggle toggle;
    public GameObject hangboardBase;
    public Transform anchor; 

    public void Start()
    {

        //Fetch the Toggle GameObject
        toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });

        if (toggle.isOn)
        {
            GameObject.Instantiate(hangboardBase, anchor);
        }
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
       if(toggle.isOn)
       {
            GameObject.Instantiate(hangboardBase, anchor); 
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

