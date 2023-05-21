using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The wooden Baseplate of the Hangboard
public class HangBoardBase : MonoBehaviour
{
    // every base has dimensions
    //the dimensions are used to verify the bounds within which a hold can be placed 
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // as the hold type sloper has bigger dimensions, the bounds for y direction need to be adapted 
    public float yMinSloper;
    public float yMaxSloper;

    // the price of a baseplate
    public float value;
    // the description of a baseplate 
    public string description = "";

    // the parent object of all instantiated holds 
    private GameObject holdAnchor;

    // the shopingCart GameObject
    private ShoppingCart shoppingCart;

    private void Start()
    {
        // whenever a base is instantiated, it checks if the currently existing holds
        // still have valid placements on the new base
        // --> necessary if we change from a big base to a small base 
        ControlHoldPlacementsOnSpawn();

        shoppingCart = GameObject.FindGameObjectWithTag("ShoppingCart").
            GetComponent<ShoppingCart>()
            ;
        // whenever a base is instantiated, update the price in the shoppingcart menu
        shoppingCart.UpdateBasePlate(this.GetComponent<HangBoardBase>());
    }


    // whenever a base is instantiated, it checks if the currently existing holds
    // still have valid placements on the new base
    // --> necessary if we change from a big base to a small base 
    private void ControlHoldPlacementsOnSpawn()
    {
        // the parent object of all holds
        holdAnchor = GameObject.FindGameObjectWithTag("HoldsAnchor");

        // loop through and determine if any of the holds is out of x y bounds
        foreach (Transform hold in holdAnchor.transform)
        {
            if (hold.position.x < xMin
                || hold.position.x > xMax
                || hold.position.y < yMin
                || hold.position.y > yMax)
            {
                hold.GetComponent<Hold>().Delete();
            }
        }
    }
}


