using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldBehaviour : MonoBehaviour
{

    public Material[] materials;
    private HoldPlacementManager holdPlacementManager;
    public bool isPlaced = false;

    private void Awake()
    {
        holdPlacementManager = GameObject.FindGameObjectWithTag("HoldPlacementManager").
            GetComponent<HoldPlacementManager>();

        GetComponent<MeshRenderer>().material = materials[0];
        transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[1];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced && other.gameObject.tag == "Hold")
        {
            holdPlacementManager.holdCanBePlaced = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[2];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isPlaced && other.gameObject.tag == "Hold")
        {
            holdPlacementManager.holdCanBePlaced = true;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[1];
        }
    }
}
