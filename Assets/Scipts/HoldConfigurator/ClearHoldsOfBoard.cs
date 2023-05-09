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
        //TODO Add a message: are you sure you want to delete all holds, if at least one hold is present 
        foreach(Transform hold in holdsAnchor.transform)
        {
            Destroy(hold.gameObject);
        }
    }
}
