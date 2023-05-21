using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    //Not used at the moment, explored as a Bugfix for the trapped scrollwheel
    //[SerializeField]
    //private Slider sliderZoom;

    // the parent GameObject of the BasePlate
    [SerializeField]
    private Transform hangboardParent;

    // offset between Camera and Hangboard
    [SerializeField]
    private Vector3 cameraOffset = new Vector3(0.1f,0.0675f,0.7f);

    //Camera Position at start
    [SerializeField]
    private Vector3 defaultCameraPosition = new Vector3(0.2f, 0.0675f, 0.7f);

    // the speed of rotation if a user is using the mouse to rotate the hangboard 
    [SerializeField]
    private float rotSpeed = 2.0f;

    // variables to store the input of the mouse axis
    private float rotY;
    private float rotX;

    // bounds and sensitivity for zooming
    private float zoomMin = 0.24f;
    private float zoomMax = 0.37f;
    private float zoomSensitivity = 0.06f;
    //the default zoom and rotation of the camera 
    private float defaultCameraZoom;
    private Quaternion defaultCameraRotation;

    // the camera animates the "reset view" with a duration of 0.5 seconds
    float animationTime = 0.5f;


    private void Start()
    {
        defaultCameraZoom = Camera.main.orthographicSize;
        defaultCameraRotation = Camera.main.transform.rotation;
        Camera.main.transform.position = defaultCameraPosition;
    }

    void Update()
    {
        //Check each frame if the user is zooming
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
        rotX = Mathf.Clamp(rotX, -35, 35);
        rotY = Mathf.Clamp(rotY, -20, 20);

        // set rotation of the camera according to mouse position 
        transform.localEulerAngles = new Vector3(rotX, rotY);
        // include an ofset to z and y ais, so that the hangboard fits the available area well
        transform.position =
            hangboardParent.position
            + transform.forward * cameraOffset.z
            - transform.right * cameraOffset.x;
    }

    //this method was used to explore alternatives to the mouse wheel zoom, it is currently not used 
    //public void ZoomWithSlider()
    //{
    //    Camera.main.orthographicSize = sliderZoom.value;
    //}

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
            // use Mathf.Smoothstep to reset the zoom to default 
            Camera.main.orthographicSize =
                Mathf.SmoothStep(
                    Camera.main.orthographicSize,
                    defaultCameraZoom,
                    timePassed / animationTime);
            // increase the time each frame by the time that passed since the last frame 
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
}