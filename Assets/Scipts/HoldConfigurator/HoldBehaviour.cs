using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldBehaviour : MonoBehaviour
{

    public Material[] materials;
    public bool isPlaced = false;
    public bool isSelected = false; 
    private HoldPlacementManager holdPlacementManager;
    private Outline hoverOutline;

    private void Awake()
    {
        holdPlacementManager = GameObject.FindGameObjectWithTag("HoldPlacementManager").
            GetComponent<HoldPlacementManager>();

        GetComponent<MeshRenderer>().material = materials[0];
        transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[1];

        hoverOutline = GetComponent<Outline>();
    }

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5)
            && hit.collider.gameObject == this.gameObject
            && !isSelected)
        {
            hoverOutline.enabled = true;
            hoverOutline.OutlineColor = Color.grey;
        }
        else if (!isSelected)
        {
            hoverOutline.enabled = false;
        }
    
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced && other.gameObject.tag == "Hold")
        {
            holdPlacementManager.holdCanBePlaced = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[2];
        }
    }

    private void OnTriggerStay(Collider other)
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
