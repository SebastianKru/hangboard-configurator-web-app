using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// HoldMenu is the menu which pop up when a hold on the board is pressed
// the HoldMenu gives information about the hold specifics via text
// it allows a user to change the material and delete a hold
public class HoldMenu : MonoBehaviour
{
    public TMP_Text heading;
    public TMP_Text description;

    // Toggles in a Toggle Group to change the Material 
    public Toggle oakToggle;
    public Toggle stoneToggle;
    public Toggle tulipToggle; 

    // fill the text fields of the hold menu with the clicked holds information 
    public void configureHoldMenu(Hold hold)
    {
        heading.text = hold.nameofHold;
        description.text = hold.descriptionofHold;
    }

    // show the correct Toggle as active once a Hold is clicked.
    // this is done by controlling which material is currently selected for a hold 
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
