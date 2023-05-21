using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGLConfigs : MonoBehaviour
{ 
    // check if the build is playing as WebGL and do not bind the Inputs solely to the Unity game.
    // this was explored to solve the scrollwheel defficency
    // but could not fix the problem yet 
    void Start()
    { 
#if (!UNITY_EDITOR) && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
#endif
    }

}
