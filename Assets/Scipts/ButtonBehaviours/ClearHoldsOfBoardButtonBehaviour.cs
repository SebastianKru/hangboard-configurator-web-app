using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearHoldsOfBoardButtonBehaviour : MonoBehaviour
{
    // the parent object off all instantiated Holds
    private GameObject holdsAnchor; 

    // find the parent of all holds by its Tag
    void Start()
    {
        holdsAnchor = GameObject.FindGameObjectWithTag("HoldsAnchor");
    }

    // iterate through all children of holdsAnchor and call the Delete() Method of class Hold
    public void OnClearHoldsButtonPressed()
    {
        foreach(Transform hold in holdsAnchor.transform)
        {
            hold.GetComponent<Hold>().Delete();
        }
    }
}
