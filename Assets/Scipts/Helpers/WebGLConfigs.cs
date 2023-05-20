using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGLConfigs : MonoBehaviour
{ 


    void Start()
    {
#if UNITY_EDITOR
        Debug.Log("editor");
#endif

#if (!UNITY_EDITOR) && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
#endif
    }

}
