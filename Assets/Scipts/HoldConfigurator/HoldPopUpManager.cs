using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldPopUpManager : MonoBehaviour
{

    public GameObject selectedHold;
    public LayerMask holdLayerMask;
    public HoldMenu holdMenu;
    private HoldPlacementManager holdPlacementManager;
    private Outline outline;


    void Start()
        {
        holdPlacementManager =
            GameObject.FindGameObjectWithTag("HoldPlacementManager").
            GetComponent<HoldPlacementManager>();

        if (holdMenu.gameObject.activeSelf)
            holdMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 5, holdLayerMask)
                //avoid selecting the currently dragged hold while it is still unplaced
                //only happens if a hold is hovering over another hold where it cannot be placed
                && hit.collider.gameObject.GetComponent<Hold>().isPlaced
                 //TODO find out why nullrefference
                 // avoid selecting a hold while dragging another hold over it 
                 && !holdPlacementManager.activelyPlacingHold
                 && !PointerOverUIElement.IsPointerOverUIElement()
                )
            {
                EnableHoldMenu(hit.collider.gameObject);
            }
            else if (selectedHold != null && !PointerOverUIElement.IsPointerOverUIElement())
            {
                DisableHoldMenu();
            }
        }

    }


    private void EnableHoldMenu(GameObject hold)
    {
        // clicking on a selected hold will deselect the hold
        if (hold == selectedHold)
        {
            DisableHoldMenu();
            return;
        }

        // clicking on a hold will deselect any other hold
        if (selectedHold != null)
            DisableHoldMenu();

        selectedHold = hold;
        //show the selectedOutline to highlight which hold is currently selected 
        selectedHold.GetComponent<Hold>().Selected();

        //enable the GameObject which holds the UI
        holdMenu.gameObject.SetActive(true);
        holdMenu.configureHoldMenu(hold.GetComponent<Hold>());
    }

    public void DisableHoldMenu()
    {
        if (selectedHold != null)
        {
            selectedHold.GetComponent<Hold>().Deselected();
            holdMenu.gameObject.SetActive(false);

            selectedHold = null;
        }
    }


    public void OnDeleteHoldButtonClicked()
    {
        selectedHold.GetComponent<Hold>().Delete();
        selectedHold = null;

        holdMenu.gameObject.SetActive(false);

    }
}
