using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 touch;

    private float minZoom = 1;
    private float maxZoom = 25;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touch = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistTouch - distTouch;
            
            Zoom(difference * 0.01f);
        }
        
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touch - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            direction.y = 0;
            mainCamera.transform.position += direction;
        } 
        
        Zoom(Input.GetAxis("Mouse ScrollWheel"));

        //transform.position = ClampCamera(transform.position);
    }

    void Zoom(float increment)
    {
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - increment, minZoom, maxZoom);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHight = mainCamera.orthographicSize;
        float camWidht = mainCamera.orthographicSize * mainCamera.aspect;

        float minX = -25 + camWidht;
        float maxX = 25 - camWidht;
        float minZ = -25 + camHight;
        float maxZ = 25 - camHight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newZ = Mathf.Clamp(targetPosition.y, minZ, maxZ);

        return new Vector3(newX, targetPosition.y, newZ);
    }
}
