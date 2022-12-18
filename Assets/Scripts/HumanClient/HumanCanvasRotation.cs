using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCanvasRotation : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera mainCamera;

    private Vector3 canvasTransformPosOnStart;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        canvasTransformPosOnStart = canvas.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.rotation = mainCamera.transform.rotation;
        canvas.transform.position = transform.position + canvasTransformPosOnStart;
    }
}
