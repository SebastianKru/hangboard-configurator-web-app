using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangBoardBase : MonoBehaviour
{
    public float xMin;
    public float xMax;

    public float yMin;
    public float yMax;

    public float yMinSloper;
    public float yMaxSloper; 

    private GameObject holdAnchor;

    private void Awake()
    {
        ControlHoldPlacementsOnSpawn();
    }

    private void ControlHoldPlacementsOnSpawn()
    {
        holdAnchor = GameObject.FindGameObjectWithTag("HoldsAnchor");

        foreach (Transform hold in holdAnchor.transform)
        {
            if (hold.position.x < xMin
                || hold.position.x > xMax
                || hold.position.y < yMin
                || hold.position.y > yMax)
            {
                Destroy(hold.gameObject);
            }
        }
    }
}


