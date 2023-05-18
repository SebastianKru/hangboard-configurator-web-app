using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 




public class ShoppingCart : MonoBehaviour
{

    public GameObject shoppingCardPopUp; 
    public TMP_Text shoppingCartButtonText;
    public TMP_Text basePlateNameText;
    public TMP_Text basePlatePriceText;

    public GameObject holdsPopUpTextParent;
    public GameObject holdTextPrefab;

    List<Hold> holds = new List<Hold>();

    public float totalPrice = 0.0f;
    private float basePlatePrice;
    private float holdsTotalPrice;

    private void Start()
    {
        if (shoppingCardPopUp.activeSelf)
            shoppingCardPopUp.SetActive(false); 
    }


    public void OnShoppingCartButtonClicked()
    {
        if(!shoppingCardPopUp.activeSelf)
            shoppingCardPopUp.SetActive(true);
        else
            shoppingCardPopUp.SetActive(false);

    }

    public void UpdateBasePlate(HangBoardBase basePlate)
    {
        basePlatePrice = basePlate.value;
        basePlatePriceText.text = basePlatePrice.ToString();

        basePlateNameText.text = basePlate.description;
        
        UpdateShoppingCartButton();
    }

    public void AddHoldToShoppingCart(Hold hold)
    {
        if(holds.Find(i => i.typeOfHold == hold.typeOfHold))
        {
            int index = holds.FindIndex(i => i.typeOfHold == hold.typeOfHold);
            holds[index].amountOfHoldsOnBoard += 1;

            

            holds[index].shoppingCartText.amount.text =
                holds[index].amountOfHoldsOnBoard.ToString()
                ;
            
        }
        else
        {
  
            GameObject g = Instantiate(holdTextPrefab, holdsPopUpTextParent.transform);
            hold.shoppingCartText = g.GetComponent<HoldShoppingCartText>();

            hold.amountOfHoldsOnBoard = 1;

            hold.shoppingCartText.amount.text = hold.amountOfHoldsOnBoard.ToString();

            hold.shoppingCartText.description.text = 
                hold.nameofHold + " " + hold.sizeofHold
                ;

            hold.shoppingCartText.price.text =
                hold.priceOfHold.ToString() + " $";
                ;

            holds.Add(hold);
        }
        
        holdsTotalPrice += hold.priceOfHold;
        UpdateShoppingCartButton();
    }

    public void RemoveHoldFromShoppingCart(Hold hold)
    {
        int index = holds.FindIndex(i => i.typeOfHold == hold.typeOfHold);

        if (holds[index].amountOfHoldsOnBoard == 1)
        {
            //hold.amountOfHoldsOnBoard = 0;
            Destroy(holds[index].shoppingCartText.gameObject);
            holds.Remove(holds[index]);
        }
        else if (holds[index].amountOfHoldsOnBoard > 1)
        {
            holds[index].amountOfHoldsOnBoard -= 1;
            holds[index].shoppingCartText.amount.text = holds[index].amountOfHoldsOnBoard.ToString();
        }

        holdsTotalPrice -= hold.priceOfHold;
        UpdateShoppingCartButton();
    }

    private void UpdateShoppingCartButton()
    {
        totalPrice = basePlatePrice + holdsTotalPrice;
        shoppingCartButtonText.text = totalPrice.ToString() + " $"; 
    }


}
