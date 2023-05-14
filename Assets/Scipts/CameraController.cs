using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform hangboardParent;

    // offset between Camera and Hangboard, as the hangboard is shifted to the right
    // in order to have anough space for the UI on the left 
    [SerializeField]
    private Vector3 cameraOffset = new Vector3(0.1f,0.0675f,0.7f);
    [SerializeField]
    private Vector3 defaultCameraPosition = new Vector3(0.2f, 0.0675f, 0.7f);
    [SerializeField]
    private float rotSpeed = 2.0f;

    private float rotY;
    private float rotX;

    private float zoomMin = 0.28f;
    private float zoomMax = 0.37f;
    private float zoomSensitivity = 0.06f;

    private float defaultCameraZoom;
    private Quaternion defaultCameraRotation;

    float animationTime = 0.5f;

    private void Start()
    {
        defaultCameraZoom = Camera.main.orthographicSize;
        defaultCameraRotation = Camera.main.transform.rotation;
        Camera.main.transform.position = defaultCameraPosition;
    }

    void Update()
    {
        //Check if the user is zooming each frame
        ZoomOnScroll();

        // if the mouse Button is pressed, the user can rotate the camera around the Hangboard 
        if (Input.GetMouseButton(0))
        {
            RotationOnDrag();
        }
    }

    private void RotationOnDrag()
    {
        // get the inputs from both mouse axis
        float mouseX = Input.GetAxis("Mouse X") * rotSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotSpeed;
        //flip the mouse axis to match in game axis
        rotY += mouseX;
        rotX += mouseY;
        // set bounds for min and max rotation on both axis 
        rotX = Mathf.Clamp(rotX, -25, 25);
        rotY = Mathf.Clamp(rotY, -25, 25);

        // set rotation of the camera according to mouse position 
        transform.localEulerAngles = new Vector3(rotX, rotY);
        // include an ofset to z and y ais, so that the hangboard fits the available area well
        transform.position =
            hangboardParent.position
            + transform.forward * cameraOffset.z
            - transform.right * cameraOffset.x;
    }

    private void ZoomOnScroll()
    {
        // zoom is done by changing the ortographic camera option called "size"
        float zoom = Camera.main.orthographicSize;

        // get the inut from mouse scroll wheel
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        // set bounds for min and max zoom
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);

        // apply the zoom value to the camera option "size"
        Camera.main.orthographicSize = zoom;
    }

    //gets called if the Button "" is pressed
    public void ResetView()
    {
        //start the animation to reset rotation and zoom
        StartCoroutine(ResetViewAnimation());

        // the variables rotY and rotX need to be reset
        // to start rotating from the center again 
        rotY = 0;
        rotX = 0;
    }

    // Coroutine 
    IEnumerator ResetViewAnimation()
    {
        float timePassed = 0.0f;

        // run through a period of animation time and smoothly change
        // rotation and zoom 
        while(timePassed < animationTime)
        {
            // use Quaternion.Slerp to change the rotation from current to default 
            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    defaultCameraRotation,
                    timePassed / animationTime);
            // use Vector3.Slerp to reset the camer position to default 
            transform.position =
                Vector3.Slerp(transform.position,
                defaultCameraPosition,
                timePassed / animationTime);

            Camera.main.orthographicSize =
                Mathf.SmoothStep(
                    Camera.main.orthographicSize,
                    defaultCameraZoom,
                    timePassed / animationTime);

            timePassed += Time.deltaTime;
            yield return null;
        }
    }
}