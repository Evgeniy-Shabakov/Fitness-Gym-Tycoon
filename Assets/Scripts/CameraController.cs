using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 touch;

    private float minZoom = 1;
    private float maxZoom = 100;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //var mousePosNearClipPlane = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane);
            //touch = mainCamera.ScreenToWorldPoint(mousePosNearClipPlane);
            
            touch = mainCamera.ScreenToViewportPoint(Input.mousePosition);
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
            
            Zoom(difference * Time.deltaTime);
        }
        
        else if (Input.GetMouseButton(0))
        {
            //var mousePosNearClipPlane = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane); 
            //Vector3 direction = touch - mainCamera.ScreenToWorldPoint(mousePosNearClipPlane);
            
            //direction.y = 0;
            //mainCamera.transform.position += direction.normalized;
            
            Vector3 direction = touch - mainCamera.ScreenToViewportPoint(Input.mousePosition);

            direction.z = direction.y;
            direction.y = 0;
            mainCamera.transform.position += direction.normalized * Time.deltaTime * mainCamera.fieldOfView / 5f;
            
            touch = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        } 
        
        Zoom(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 500);

        //transform.position = ClampCamera(transform.position);
    }

    void Zoom(float increment)
    {
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - increment, minZoom, maxZoom);
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
