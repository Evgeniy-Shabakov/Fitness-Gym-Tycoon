using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreBuildingMoving : MonoBehaviour
{
    private bool cameraPositionIsChanged;
    private Vector3 cameraPositionMouseDown;

    private bool movingClickAllowed;

    private Vector3 touch;
    
    private GameObject parent;

    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    

    private void OnMouseDrag()
    {
        movingClickAllowed = false;
        Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
        
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, BuildingManager.Instanse.layerMaskForBuilddAllowed);

        parent.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
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
            
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit; 
            Physics.Raycast(ray, out hit, 100f, BuildingManager.Instanse.layerMaskForBuilddAllowed);

            parent.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
    }
}