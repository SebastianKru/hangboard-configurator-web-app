using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ShoppingCart : MonoBehaviour
{

    public TMP_Text shoppingCartButtonText;
    public TMP_Text basePlateNameText;
    public TMP_Text basePlatePriceText;



    public float totalPrice = 0.0f;
    private float basePlatePrice;
    private float holdsTotalPrice;


    public void UpdateBasePlate(HangBoardBase basePlate)
    {
        basePlatePrice = basePlate.value;
        basePlatePriceText.text = basePlatePrice.ToString();

        basePlateNameText.text = basePlate.description;
        
        UpdateShoppingCartButton();
    }

    public void AddHoldToShoppingCart(Hold hold)
    {
        holdsTotalPrice += hold.value;
        UpdateShoppingCartButton();
    }

    public void RemoveHoldFromShoppingCart(Hold hold)
    {
        holdsTotalPrice -= hold.value;
        UpdateShoppingCartButton();
    }

    private void UpdateShoppingCartButton()
    {
        totalPrice = basePlatePrice + holdsTotalPrice;
        shoppingCartButtonText.text = totalPrice.ToString() + " $"; 
    }
}
