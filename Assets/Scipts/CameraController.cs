using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform hangboardParent;
    [SerializeField]
    private Vector3 cameraOffset = new Vector3(0.09f,0,0.7f);
    [SerializeField]
    private float rotSpeed = 2.0f;
    private float rotY;
    private float rotX;

    private float zoomMin = 0.2f;
    private float zoomMax = 0.37f;
    private float zoomSensitivity = 0.06f; 

    void Update()
    {
        //Check if the user is zooming each frame
        ZoomBehaviourOnScroll();

        // if the mouse Button is pressed, the user can rotate the camera around the Hangboard 
        if (Input.GetMouseButton(0))
        {
            RotationBehaviourOnDrag();
        }
    }

    private void RotationBehaviourOnDrag()
    {
        // get the inputs from both mouse axis
        float mouseX = Input.GetAxis("Mouse X") * rotSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotSpeed;

        //flip the mouse axis to match in game axis 
        rotY += mouseX;
        rotX += mouseY;

        // set bounds for min and max rotation on both axis 
        rotX = Mathf.Clamp(rotX, -40, 40);
        rotY = Mathf.Clamp(rotY, -40, 40);

        // set rotation of the camera according to mouse position 
        transform.localEulerAngles = new Vector3(rotX, rotY);

        // include an ofset to z and y ais, so that the hangboard fits the available area well
        transform.position =
            hangboardParent.position
            - transform.forward * cameraOffset.z
            - transform.right * cameraOffset.x;
    }

    private void ZoomBehaviourOnScroll()
    {
        // zoom is done by changing the ortographic camera option called "size"
        float zoom = Camera.main.orthographicSize;

        // get the inut from mouse scroll wheel
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        // set bounds for min and max zoom
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);

        //apply the zoom value to the camera option "size"
        Camera.main.orthographicSize = zoom;
    }
}


