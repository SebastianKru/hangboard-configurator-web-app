using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

// manages all interactions with the ShoppingCart
public class ShoppingCart : MonoBehaviour
{
    // the UI of the ShoppingCartMenu
    public GameObject shoppingCardPopUp;

    public TMP_Text shoppingCartButtonText;
    // text and price of the base plate
    public TMP_Text basePlateNameText;
    public TMP_Text basePlatePriceText;

    // the parent object who will align all hold text gameobjects in the shopping cart ui
    public GameObject holdsShoppingcartTextParent;
    // prefab to instantiate for the hold text description
    public GameObject holdTextPrefab;

    // save all holds which are spawned on the base (and added to the shopping cart)
    // in a list
    List<Hold> holds = new List<Hold>();

    // the total price of all items selected by the user
    public float totalPrice = 0.0f;

    private float basePlatePrice;
    private float holdsTotalPrice;

    // disable the hold menu on start
    private void Start()
    {
        if (shoppingCardPopUp.activeSelf)
            shoppingCardPopUp.SetActive(false); 
    }

    // enable / disable the menu on button click
    public void OnShoppingCartButtonClicked()
    {
        if(!shoppingCardPopUp.activeSelf)
            shoppingCardPopUp.SetActive(true);
        else
            shoppingCardPopUp.SetActive(false);

    }

    // manages the changes of baseplates by the user
    public void UpdateBasePlate(HangBoardBase basePlate)
    {
        basePlatePrice = basePlate.value;
        basePlatePriceText.text = basePlatePrice.ToString();
        basePlateNameText.text = basePlate.description;
        
        UpdateShoppingCartButton();
    }


    public void AddHoldToShoppingCart(Hold hold)
    {
        // if the newly placed hold on the hangboard is already existing
        // (the enum is already present on the board)
        if(holds.Find(i => i.typeOfHold == hold.typeOfHold))
        {
            // the find the index of this already existing hold
            int index = holds.FindIndex(i => i.typeOfHold == hold.typeOfHold);
            // increase the amount of this hold by one
            holds[index].amountOfHoldsOnBoard += 1;

            // update the text to match the current amount
            holds[index].shoppingCartText.amount.text =
                holds[index].amountOfHoldsOnBoard.ToString()
                ;
            
        }
        else
        {
            // if this type of hold does not yet exist, instantiate the description text in the shopping cart 
            GameObject g = Instantiate(holdTextPrefab, holdsShoppingcartTextParent.transform);
            hold.shoppingCartText = g.GetComponent<HoldShoppingCartText>();

            // set the amount to 1
            hold.amountOfHoldsOnBoard = 1;
            hold.shoppingCartText.amount.text = hold.amountOfHoldsOnBoard.ToString();

            // set the description according to the values of the hold
            hold.shoppingCartText.description.text = 
                hold.nameofHold + " " + hold.sizeofHold
                ;

            hold.shoppingCartText.price.text =
                hold.priceOfHold.ToString() + " $";
                ;

            // add the hold to the list of holds
            holds.Add(hold);
        }
        // update the total price of all holds
        holdsTotalPrice += hold.priceOfHold;
        UpdateShoppingCartButton();
    }

    public void RemoveHoldFromShoppingCart(Hold hold)
    {
        // find the hold in the list of holds
        int index = holds.FindIndex(i => i.typeOfHold == hold.typeOfHold);

        // of only one of this type of holds exists
        if (holds[index].amountOfHoldsOnBoard == 1)
        {
            // Destroy the hold description text and remove the hold from the list 
            Destroy(holds[index].shoppingCartText.gameObject);
            holds.Remove(holds[index]);
        }
        // if more than one of this type of hold exist, simply count down by one
        else if (holds[index].amountOfHoldsOnBoard > 1)
        {
            holds[index].amountOfHoldsOnBoard -= 1;
            holds[index].shoppingCartText.amount.text = holds[index].amountOfHoldsOnBoard.ToString();
        }
        // remove the price value from total value
        holdsTotalPrice -= hold.priceOfHold;
        UpdateShoppingCartButton();
    }

    private void UpdateShoppingCartButton()
    {
        totalPrice = basePlatePrice + holdsTotalPrice;
        shoppingCartButtonText.text = totalPrice.ToString() + " $"; 
    }


}
