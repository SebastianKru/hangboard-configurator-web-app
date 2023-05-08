using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlacementManager : MonoBehaviour
{


    public GameObject[] holds;
    public GameObject holdsParent;
    private GameObject holdCur; 

    private Vector3 pos;
    private RaycastHit raycastHit;

    public LayerMask layerMask;

    public float gridSizeX = 0.1f;
    public float gridSizeY = 0.055f;

    private float thicknessBase = 0.025f;

    private HangBoardBase hangBoardBase;



    private void Start()
    {

    }

    private void Update()
    {
        if (holdCur != null)
        {
            holdCur.transform.position = new Vector3(
                RoundToNearestGrid(pos.x, gridSizeX, hangBoardBase.xMin, hangBoardBase.xMax),
                RoundToNearestGrid(pos.y, gridSizeY, hangBoardBase.yMin, hangBoardBase.yMax),
                -(thicknessBase + holdCur.GetComponent<BoxCollider>().bounds.size.z) / 2
                );

            if(Input.GetMouseButtonDown(0))
            {
                holdCur = null; 
            }
        }
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, 5f, layerMask))
        {
            pos = raycastHit.point;
        }
    }

    public void DragHold(int index)
    {
        holdCur = Instantiate(holds[index], pos, transform.rotation, holdsParent.transform);
        hangBoardBase = GetComponentInChildren<HangBoardBase>();
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