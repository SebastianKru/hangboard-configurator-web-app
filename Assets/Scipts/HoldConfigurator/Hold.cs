using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public int selectedMaterial = 3; 

    public Material[] materials;

    public enum TypeOfHold {crimp10mm, crimp15mm, crimp20mm, pocketL, pocketM, pocketS, sloper25, sloper35, sloper45 };

    public TypeOfHold typeOfHold;

    public HoldShoppingCartText shoppingCartText; 

    public string nameofHold = "Crimp";
    public string sizeofHold = "10mm";
    public string descriptionofHold = "a wooden crimp with 2mm edge";
    public float priceOfHold = 0.0f;
    public int amountOfHoldsOnBoard = 0;

    public bool isPlaced = false;
    public bool isSelected = false;

    private HoldPlacementManager holdPlacementManager;
    private Outline outline;

    private ShoppingCart shoppingCart; 

    private void Awake()
    {
        holdPlacementManager = GameObject.FindGameObjectWithTag("HoldPlacementManager").
            GetComponent<HoldPlacementManager>();

        shoppingCart = GameObject.FindGameObjectWithTag("ShoppingCart").
            GetComponent<ShoppingCart>()
            ;
        

        materials[0] = materials[3]; 
        GetComponent<MeshRenderer>().material = materials[0];
        transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[1];

        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        HoverHighlight(ray);
    }



    public void PlaceThisHold()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        isPlaced = true;
        shoppingCart.AddHoldToShoppingCart(this.GetComponent<Hold>());
    }



    private void HoverHighlight(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5)
            && hit.collider.gameObject == this.gameObject
            && !isSelected
            && !PointerOverUIElement.IsPointerOverUIElement()
            )
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

    public void Delete()
    {
        shoppingCart.RemoveHoldFromShoppingCart(this.GetComponent<Hold>());
        Deselected();
        Destroy(this.gameObject);
    }

    public void ChangeHoldMaterial(int matIndex)
    {
        materials[0] = materials[matIndex];
        selectedMaterial = matIndex;

        GetComponent<MeshRenderer>().material = materials[0];
    }
}
