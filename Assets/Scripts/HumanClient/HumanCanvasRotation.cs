using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCanvasRotation : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.rotation = mainCamera.transform.rotation;
    }
}
