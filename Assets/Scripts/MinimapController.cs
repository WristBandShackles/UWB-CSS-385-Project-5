using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{

    public GameObject minimapCanvas;

    // Start is called before the first frame update
    void Start()
    {
        ToggleMinimap(false);
    }

    public void ToggleMinimap(bool showMinimap)
    {
        minimapCanvas.SetActive(showMinimap);
    }
}
