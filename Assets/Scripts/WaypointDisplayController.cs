using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaypointDisplayController : MonoBehaviour
{
    public TMP_Text textComponent;
    public Camera minimapCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string cameraActive = "false";

        if (minimapCamera.transform.position.x != 300 && minimapCamera.transform.position.y != 300)
        {
            cameraActive = "true";
        }

        textComponent.text = "WayPoint Cam: " + cameraActive;
    }
}
