using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Source https://forum.unity.com/threads/how-to-detect-if-mouse-is-over-ui.1025533/
// a helper script, which detects if the mouse if over a UI element
// only slightly adapted by me, to pass the layer of UI element as int

public class PointerOverUIElement : MonoBehaviour
{

    //Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(int layeruI)
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults(), layeruI);
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults, int layerUI)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == layerUI)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
