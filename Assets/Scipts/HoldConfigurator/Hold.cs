using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{

    public Material[] materials;

    public string nameofHold = "Crimp";
    public string sizeofHold = "10mm";
    public string descriptionofHold = "a wooden crimp with 2mm edge";

    public bool isPlaced = false;
    public bool isSelected = false; 
    private HoldPlacementManager holdPlacementManager;
    private Outline outline;

    private void Awake()
    {
        holdPlacementManager = GameObject.FindGameObjectWithTag("HoldPlacementManager").
            GetComponent<HoldPlacementManager>();

        GetComponent<MeshRenderer>().material = materials[0];
        transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[1];

        outline = GetComponent<Outline>();
    }

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        HoverHighlight(ray); 
    }

    private void HoverHighlight(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5)
            && hit.collider.gameObject == this.gameObject
            && !isSelected)
        {
            outline.enabled = true;
            outline.OutlineColor = Color.grey;
        }
        else if (!isSelected)
        {
            outline.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        GiveFeedbackForPlacement(other, false, 2);
    }

    private void OnTriggerStay(Collider other)
    {
        GiveFeedbackForPlacement(other, false, 2);
    }

    private void OnTriggerExit(Collider other)
    {
        GiveFeedbackForPlacement(other, true, 1);
    }

    private void GiveFeedbackForPlacement(Collider other, bool placeAvailable, int material)
    {
        if (!isPlaced && other.gameObject.tag == "Hold")
        {
            holdPlacementManager.isPlaceAvailable = placeAvailable;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[material];
        }
    }


    public void Deselected()
    {
        outline.enabled = false;
        isSelected = false;
    }

    public void Selected()
    {
        outline.enabled = true;
        outline.OutlineColor = Color.white;
        isSelected = true;
    }
}
