using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoldMenu : MonoBehaviour
{

    public TMP_Text heading;
    public TMP_Text description;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void configureHoldMenu(Hold hold)
    {
        heading.text = hold.nameofHold;
        description.text = hold.descriptionofHold;
    }
}
