using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearHoldsOfBoard : MonoBehaviour
{

    private GameObject holdsAnchor; 
    // Start is called before the first frame update
    void Start()
    {
        holdsAnchor = GameObject.FindGameObjectWithTag("HoldsAnchor");
    }

    public void OnClearHoldsButtonPressed()
    {
        foreach(Transform hold in holdsAnchor.transform)
        {
            hold.GetComponent<Hold>().Delete();
        }
    }
}
