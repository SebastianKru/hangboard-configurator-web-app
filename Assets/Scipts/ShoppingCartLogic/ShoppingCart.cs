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
            Debug.Log("Hold already dispolayed");
        }
        else
        {
            holds.Add(hold);

            hold.shoppingCartText =
                Instantiate(holdTextPrefab, holdsPopUpTextParent.transform
                );

            hold.shoppingCartText.transform.GetChild(0).
                GetComponent<TMP_Text>().text =
                hold.nameofHold + " " + hold.sizeofHold
                ;

            hold.shoppingCartText.transform.GetChild(2).
                GetComponent<TMP_Text>().text =
                hold.priceOfHold.ToString() + " $";
                ;
        }








        holdsTotalPrice += hold.priceOfHold;
        UpdateShoppingCartButton();
    }

    public void RemoveHoldFromShoppingCart(Hold hold)
    {
        holdsTotalPrice -= hold.priceOfHold;
        UpdateShoppingCartButton();
    }

    private void UpdateShoppingCartButton()
    {
        totalPrice = basePlatePrice + holdsTotalPrice;
        shoppingCartButtonText.text = totalPrice.ToString() + " $"; 
    }


}
