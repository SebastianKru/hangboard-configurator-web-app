using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to handle the spawning and placing of a hold on the board 
public class HoldPlacementManager : MonoBehaviour
{

    // this is filled with all hold prefabs in the Unity Editor 
    public GameObject[] holds;
    // the parent GameObject of the holds
    public GameObject holdsParent;
    // the currently selected hold (to be placed)
    public GameObject holdCur;

    public bool activelyPlacingHold = false;

    //position of the mouse over the hangboard
    private Vector3 mousePosOverHangboard;
    private RaycastHit raycastHit;

    // the Layermask to identify the baseplate
    public LayerMask layerMask;

    // custom variables to determine the size of the placement grid
    public float gridSizeX = 0.1f;
    public float gridSizeY = 0.055f;
    public float gridSizeYSloper = 0.12f;

    // thickness of the wooden baseplate
    private float thicknessBase = 0.025f;

    private HangBoardBase hangBoardBase;

    //is this place already occupied by another hold? 
    public bool isPlaceAvailable = true;

    // is the mouse over the hangboard base, when the user wants to place the hold? 
    private bool isMouseOverHangboard = false;

    // holds were modelled in Fusion360 with a 180 degree wrong orientation on the y axis
    // this is fixed and roatated to correct rotation on spawn 
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
        // two different grid spacings are used depending if a sloper or a smaller hold is placed
        if (holdCur.GetComponent<Hold>().nameofHold == "sloper")
        {
            holdCur.transform.position = new Vector3(
                RoundToNearestGrid(mousePosOverHangboard.x, gridSizeX, hangBoardBase.xMin, hangBoardBase.xMax),
                RoundToNearestGrid(mousePosOverHangboard.y, gridSizeYSloper, hangBoardBase.yMinSloper, hangBoardBase.yMaxSloper),
                -thicknessBase / 2
                );
        }
        else
        {
            holdCur.transform.position = new Vector3(
                RoundToNearestGrid(mousePosOverHangboard.x, gridSizeX, hangBoardBase.xMin, hangBoardBase.xMax),
                RoundToNearestGrid(mousePosOverHangboard.y, gridSizeY, hangBoardBase.yMin, hangBoardBase.yMax),
                -thicknessBase / 2
                );
        }

        if (Input.GetMouseButtonDown(0) && isMouseOverHangboard)
        {
            if (isPlaceAvailable)
            {
                // deselect hold on placement, as we do not want the popUp Menu to open on placement 
                holdsParent.GetComponent<HoldPopUpManager>().DisableHoldMenu();
                // call the pacethisHold method of the cur hold
                holdCur.GetComponent<Hold>().PlaceThisHold();
                // set cur hold null after it was placed
                holdCur = null;

                activelyPlacingHold = false;
            }
        }
        // if the user is pressing the mouse button and he is not hovering over the baseplate,
        // the hold gets deleted
        else if (Input.GetMouseButtonDown(0) && !isMouseOverHangboard)
        {
            holdCur.GetComponent<Hold>().Delete();
        }
    }

    // using FixedUopdate to check of the mouse is over the baseplate
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, 5f, layerMask))
        {
            mousePosOverHangboard = raycastHit.point;
            isMouseOverHangboard = true;
        }
        else
            isMouseOverHangboard = false; 
    }

    // instantiate the hold and make it follow the mouse position
    public void DragHold(int index)
    {
        //control if a hold is currently being placed
        if(holdCur == null)
        {
            holdCur = Instantiate(holds[index], mousePosOverHangboard, Quaternion.Euler(holdRotation), holdsParent.transform);
            activelyPlacingHold = true;
            hangBoardBase = GetComponentInChildren<HangBoardBase>();
        }
    }

    // takes the mouse position as input and rounds to the nearest matching grid position
    private float RoundToNearestGrid(float pos, float gridSize, float minBound, float maxBound)
    {
        float difference = pos % gridSize;
        pos -= difference;
        if (difference > (gridSize / 2))
        {
            pos += gridSize;
        }

        // if we get out of bounds, the max and min bounds are set as placement position
        if (pos >= maxBound)
            pos = maxBound;

        if(pos <= minBound)
            pos = minBound;

        return pos;
    }
}
