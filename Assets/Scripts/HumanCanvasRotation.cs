using UnityEngine;

public class HumanCanvasRotation : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera _mainCamera;

    private Vector3 _canvasTransformPosOnStart;
    
    void Start()
    {
        _mainCamera = Camera.main;
        _canvasTransformPosOnStart = canvas.transform.localPosition;
    }

    void Update()
    {
        canvas.transform.rotation = _mainCamera.transform.rotation;
        canvas.transform.position = transform.position + _canvasTransformPosOnStart;
    }
}
