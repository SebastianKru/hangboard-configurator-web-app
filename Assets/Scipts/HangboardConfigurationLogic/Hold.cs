using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the class for all Holds 
public class Hold : MonoBehaviour
{

    // the currently active material
    public int selectedMaterial = 3; 

    // an array of materials, containing the highlights and different physical materials
    public Material[] materials;

    // all type of holds 
    public enum TypeOfHold {crimp10mm, crimp15mm, crimp20mm, pocketL, pocketM, pocketS, sloper25, sloper35, sloper45 };
    public TypeOfHold typeOfHold;

    // the class to handle the description of Holds in the shopping cart overview
    public HoldShoppingCartText shoppingCartText; 

    // descriptions of the hold
    public string nameofHold = "Crimp";
    public string sizeofHold = "10mm";
    public string descriptionofHold = "a wooden crimp with 2mm edge";
    public float priceOfHold = 0.0f;

    // each type of hold counts how many holds of its kind exist on the current board
    // used to display the amount of holds in the shopping cart
    public int amountOfHoldsOnBoard = 0;

    // flag to check if a hold is placed on the board or still inPlacement by the user
    public bool isPlaced = false;
    // flag to determine if a user clicked on a hold
    public bool isSelected = false;

    // the manager script which is responsible for placing the hold
    private HoldPlacementManager holdPlacementManager;

    // the outline highlight component of each hold
    private Outline outline;

    // the shopping cart class
    private ShoppingCart shoppingCart; 

    private void Awake()
    {
        holdPlacementManager = GameObject.FindGameObjectWithTag("HoldPlacementManager").
            GetComponent<HoldPlacementManager>()
            ;

        shoppingCart = GameObject.FindGameObjectWithTag("ShoppingCart").
            GetComponent<ShoppingCart>()
            ;
        
        // onAwake of the GameoBject we set the material in slot 3 as the default material
        materials[0] = materials[3]; 
        GetComponent<MeshRenderer>().material = materials[0];
        // onAwake we set the colour of the placementhighlight to green
        // the placementhighlight is the box which is green if the placement is possible
        // and red if it is not possible 
        transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[1];

        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        // shoot a ray from the mouse position forward
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        HoverHighlight(ray);
    }


    // called once a Hold has been placed
    public void PlaceThisHold()
    {
        // disable the highlight component
        transform.GetChild(0).gameObject.SetActive(false);
        isPlaced = true;
        // add the hold to the shoppingcart
        shoppingCart.AddHoldToShoppingCart(this.GetComponent<Hold>());
    }


    // the highlight outline which is shown if the mouse is over a placed hold
    private void HoverHighlight(Ray ray)
    {
        RaycastHit hit;
        // if the mouse if over an object
        if (Physics.Raycast(ray, out hit, 5)
            // and the object is this hold
            && hit.collider.gameObject == this.gameObject
            // and this hold is not curently selected
            && !isSelected
            // and the mouse is not simultanously over the UI Element "HoldMenu"
            && !PointerOverUIElement.IsPointerOverUIElement(7)
            )
        {
            // the outline highlight is enabled and set to colour grey
            outline.enabled = true;
            outline.OutlineColor = Color.grey;
        }
        // if the hold is not currently selected, the outline is disabled
        else if (!isSelected)
        {
            outline.enabled = false;
        }
    }

    // ONTrigger functions are used to check if the place is a valid place to place the hold
    // false if another trigger is entered
    private void OnTriggerEnter(Collider other)
    {
        GiveFeedbackForPlacement(other, false, 2);
    }

    // false if it is resting on another trigger
    private void OnTriggerStay(Collider other)
    {
        GiveFeedbackForPlacement(other, false, 2);
    }

    //true if not over another trigger / exiting a trigger
    private void OnTriggerExit(Collider other)
    {
        GiveFeedbackForPlacement(other, true, 1);
    }

    // give the user a visual feedback if the placement is possible
    // by selecting the corret colour of the highlight material
    // and switching the flac "isPlaceAvailable" between true and false
    private void GiveFeedbackForPlacement(Collider other, bool placeAvailable, int material)
    {
        if (!isPlaced && other.gameObject.tag == "Hold")
        {
            holdPlacementManager.isPlaceAvailable = placeAvailable;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[material];
        }
    }

    // called if a hold is deselected
    public void Deselected()
    {
        outline.enabled = false;
        isSelected = false;
    }

    //called if a hold is selected
    public void Selected()
    {
        outline.enabled = true;
        outline.OutlineColor = Color.white;
        isSelected = true;
    }

    // called when a hold gameobject should be destroyed
    public void Delete()
    {
        // hold is removed from the shopping cart
        shoppingCart.RemoveHoldFromShoppingCart(this.GetComponent<Hold>());
        Deselected();
        Destroy(this.gameObject);
    }

    // helper method to change the material of a hold
    public void ChangeHoldMaterial(int matIndex)
    {
        materials[0] = materials[matIndex];
        selectedMaterial = matIndex;

        GetComponent<MeshRenderer>().material = materials[0];
    }
}
