using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// handles the logic of the HoldMeu
public class HoldPopUpManager : MonoBehaviour
{
    // currently selected Hold on the Hangboard
    public GameObject selectedHold;

    public LayerMask holdLayerMask;

    // The UI of the HoldMenu
    public HoldMenu holdMenu;

    private HoldPlacementManager holdPlacementManager;
    private Outline outline;

    void Start()
    {
        holdPlacementManager =
            GameObject.FindGameObjectWithTag("HoldPlacementManager").
            GetComponent<HoldPlacementManager>();

        // disable the holdMenu on start 
        if (holdMenu.gameObject.activeSelf)
            holdMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // if the mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // if the mouse is over a hold
            if (Physics.Raycast(ray, out hit, 5, holdLayerMask)
                //avoid selecting the currently dragged hold while it is still unplaced
                //only happens if a hold is hovering over another hold where it cannot be placed
                && hit.collider.gameObject.GetComponent<Hold>().isPlaced
                 // avoid selecting a hold while dragging another hold over it 
                 && !holdPlacementManager.activelyPlacingHold
                 // ignore the click if the mouse is simoultanously over the holdMenu
                 && !PointerOverUIElement.IsPointerOverUIElement(7)
                )
            {
                EnableHoldMenu(hit.collider.gameObject);
            }
            // if the user clicks anywhere else
            else if (selectedHold != null
                // and if he does not click on the menu itself
                && !PointerOverUIElement.IsPointerOverUIElement(7)
                )
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
        //set the position so that the menu is placed within the bounds of the webPlayer
        holdMenu.transform.position = SetValidPlacementForMenu(selectedHold);
        //control which material is currently selected and select the correspding toggle 
        holdMenu.ShowToggleOfActiveMaterial(selectedHold.GetComponent<Hold>().selectedMaterial);
        //Setup the text to display within the menu
        holdMenu.configureHoldMenu(selectedHold.GetComponent<Hold>());
    }


    // disable the menu and switch the highlight outline to deselected of the hold
    public void DisableHoldMenu()
    {
        if (selectedHold != null)
        {
            selectedHold.GetComponent<Hold>().Deselected();
            holdMenu.gameObject.SetActive(false);

            selectedHold = null;
        }
    }

    // if the user clicks the " delete " button of the HoldMenu
    public void OnDeleteHoldButtonClicked()
    {
        selectedHold.GetComponent<Hold>().Delete();
        selectedHold = null;

        holdMenu.gameObject.SetActive(false);

    }

    //Change hold material according to the Toggle
    public void OnChangeMaterialToggleClicked(int index)
    {
        selectedHold.GetComponent<Hold>().ChangeHoldMaterial(index);
    }

    // the hold menu should not open up at the same place all the time
    // as it is easier to connect the menu to a specific clicked old if it shows up close to the hold
    // however, if a hold is close to the edge of the gameview, it could be opened partially outside or over / under another UI element
    // therefore we set bounds for valid placements of the menu
    private Vector3 SetValidPlacementForMenu(GameObject hold)
    {
        //Transform the world coordinates of a hold to the screen coordinates of a UI element
        Vector3 menuPos = RectTransformUtility.
            WorldToScreenPoint(Camera.main, hold.transform.position);

        // per default the holdMenu us placed straight under the hold.
        // if the hold is to close to the borders of the web application,
        // the position gets addapted, so that the menu always stays within the application borders
        if (menuPos.y < 185)
            menuPos.y += 250;

        if (menuPos.x < 800)
            menuPos.x = 800;
        
        else if (menuPos.x > 2360)
            menuPos.x = 2360;

        return menuPos;
    }
}
