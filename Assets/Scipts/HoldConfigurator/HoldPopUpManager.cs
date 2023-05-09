using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPopUpManager : MonoBehaviour
{

    public GameObject selectedHold;
    public LayerMask holdLayerMask;
    private HoldPlacementManager holdPlacementManager;
    private Outline outline; 



    // Start is called before the first frame update
    void Start()
    {
        holdPlacementManager = GameObject.FindGameObjectWithTag("HoldsAnchor").
            GetComponent<HoldPlacementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, 5, holdLayerMask)
                //avoid selecting the currently dragged hold while it is still unplaced
                //only happens if a hold is hovering over another hold where it cannot be placed
                && hit.collider.gameObject.GetComponent<HoldBehaviour>().isPlaced
                //TODO find out why nullrefference
                // avoid selecting a hold while dragging another hold over it 
                // && !holdPlacementManager.activelyPlacingHold
                )
            {
                Select(hit.collider.gameObject);
            }
            else if (selectedHold!= null)
            {
                Deselect();
            }
        }

    }

    private void Select(GameObject hold)
    {
        // clicking on a selected hold will deselect the hold
        if (hold == selectedHold)
        {
            Deselect();
            return;
        }

        // clicking on a hold will deselect any other hold
        if(selectedHold != null)
            Deselect();
        

        outline = hold.GetComponent<Outline>();
        outline.enabled = true;
        outline.OutlineColor = Color.white;
        selectedHold = hold;
        selectedHold.GetComponent<HoldBehaviour>().isSelected = true; 
    }

    public void Deselect()
    {
        if (selectedHold != null)
        {
            selectedHold.GetComponent<Outline>().enabled = false;
            selectedHold.GetComponent<HoldBehaviour>().isSelected = false;
            selectedHold = null;
        }
    }
}
