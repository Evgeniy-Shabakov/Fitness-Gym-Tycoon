using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreBuildingMoving : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private bool cameraPositionIsChanged;
    private Vector3 cameraPositionMouseDown;

    private bool movingClickAllowed;

    private void OnMouseDrag()
    {
        movingClickAllowed = false;
        Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
        
        var mousePosNearClipPlane = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        var touch = Camera.main.ScreenToWorldPoint(mousePosNearClipPlane);

        Vector3 dir = touch - Camera.main.transform.position;
        
        Ray ray = new Ray(Camera.main.transform.position, dir);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, layerMask);

        gameObject.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
    }

    private void OnMouseUp()
    {
        Camera.main.gameObject.GetComponent<CameraController>().enabled = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject ())
            {
                movingClickAllowed = false;
            }

            else
            {
                movingClickAllowed = true;
                cameraPositionMouseDown = Camera.main.transform.position;
            }
        }
        
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                movingClickAllowed = false;
            }

            else
            {
                movingClickAllowed = true;
                cameraPositionMouseDown = Camera.main.transform.position;
            }
        }

        if (Input.touchCount >= 2)
        {
            movingClickAllowed = false;
        }

        if (Input.GetMouseButtonUp(0) && movingClickAllowed)
        {
            if (Camera.main.transform.position != cameraPositionMouseDown) return;
            
            var mousePosNearClipPlane = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            var touch = Camera.main.ScreenToWorldPoint(mousePosNearClipPlane);

            Vector3 dir = touch - Camera.main.transform.position;
            
            Ray ray = new Ray(Camera.main.transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                gameObject.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
            }
        }
    }
}
