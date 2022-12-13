using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHumanClient : MonoBehaviour
{
    private GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    private void OnMouseUpAsButton()
    {
        canvas.transform.Find("PanelHumanClient").gameObject.SetActive(true);
        canvas.transform.Find("ButtonExitPanelHumanClient").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
