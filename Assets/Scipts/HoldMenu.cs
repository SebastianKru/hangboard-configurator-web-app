using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HoldMenu : MonoBehaviour
{
    public TMP_Text heading;
    public TMP_Text description;

    public Toggle oakToggle;
    public Toggle stoneToggle;
    public Toggle tulipToggle; 

    public void configureHoldMenu(Hold hold)
    {
        heading.text = hold.nameofHold;
        description.text = hold.descriptionofHold;

    }

    public void ShowToggleOfActiveMaterial(int matIndex)
    {
        switch(matIndex)
        {
            case 1:
                oakToggle.isOn = true;
                break;
            case 3:
                oakToggle.isOn = true;
                break;
            case 4:
                stoneToggle.isOn = true;
                break;
            case 5:
                tulipToggle.isOn = true;
                break; 
        }
    }
}
