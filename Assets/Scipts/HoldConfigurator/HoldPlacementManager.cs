using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlacementManager : MonoBehaviour
{


    public GameObject[] holds;
    public GameObject holdsParent;
    public GameObject holdCur;
    public bool activelyPlacingHold = false; 

    private Vector3 pos;
    private RaycastHit raycastHit;

    public LayerMask layerMask;

    public float gridSizeX = 0.1f;
    public float gridSizeY = 0.055f;

    private float thicknessBase = 0.025f;

    private HangBoardBase hangBoardBase;

    //is this place already occupied by another hold? 
    public bool isPlaceAvailable = true;

    // is the mouse over the hangboard base, when the user wants to place the hold? 
    private bool isMouseOverHangboard = false;

    private Vector3 holdRotation = new Vector3(0, 180, 0); 


    private void Update()
    {
        if (holdCur != null)
        {
            PlaceHold();
        }
    }
    private void PlaceHold()
    {
        holdCur.transform.position = new Vector3(
            RoundToNearestGrid(pos.x, gridSizeX, hangBoardBase.xMin, hangBoardBase.xMax),
            RoundToNearestGrid(pos.y, gridSizeY, hangBoardBase.yMin, hangBoardBase.yMax),
            -thicknessBase/2
            //-(thicknessBase + holdCur.GetComponent<BoxCollider>().bounds.size.z) / 2
            );

        if (Input.GetMouseButtonDown(0) && isMouseOverHangboard)
        {
            if (isPlaceAvailable)
            {
                holdCur.transform.GetChild(0).gameObject.SetActive(false);
                holdCur.GetComponent<Hold>().isPlaced = true;
                // deselect hold on placement, as we do not weant the popUp Menu to open on placement 
                holdsParent.GetComponent<HoldPopUpManager>().DisableHoldMenu();
                holdCur = null;
                activelyPlacingHold = false;
            }
        }
        else if (Input.GetMouseButtonDown(0) && !isMouseOverHangboard)
        {
            Destroy(holdCur);
        }
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, 5f, layerMask))
        {
            pos = raycastHit.point;
            isMouseOverHangboard = true;
        }
        else
            isMouseOverHangboard = false; 
    }

    public void DragHold(int index)
    {
        //control if a hold is currently being placed
        if(holdCur == null)
        {
            holdCur = Instantiate(holds[index], pos, Quaternion.Euler(holdRotation), holdsParent.transform);
            activelyPlacingHold = true;
            hangBoardBase = GetComponentInChildren<HangBoardBase>();
        }
    }


    private float RoundToNearestGrid(float pos, float gridSize, float minBound, float maxBound)
    {
        float difference = pos % gridSize;
        pos -= difference;
        if (difference > (gridSize / 2))
        {
            pos += gridSize;
        }

        if (pos >= maxBound)
            pos = maxBound;

        if(pos <= minBound)
            pos = minBound;

        return pos;
    }
}
