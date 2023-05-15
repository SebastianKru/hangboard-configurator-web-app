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
            else if (selectedHold != null
                && !PointerOverUIElement.IsPointerOverUIElement()
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

    public void OnChangeMaterialToggleClicked(int index)
    {
        selectedHold.GetComponent<Hold>().ChangeHoldMaterial(index);
    }

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
