using UnityEngine;
using UnityEngine.EventSystems;

public class PreBuildingMoving : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private bool cameraPositionIsChanged;
    private Vector3 cameraPositionMouseDown;

    private bool movingAllowed;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject ())
            {
                movingAllowed = false;
            }

            else
            {
                movingAllowed = true;
                cameraPositionMouseDown = Camera.main.transform.position;
            }
        }
        
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                movingAllowed = false;
            }

            else
            {
                movingAllowed = true;
                cameraPositionMouseDown = Camera.main.transform.position;
            }
        }
        
        if (Input.GetMouseButtonUp(0) && movingAllowed)
        {
            if (Camera.main.transform.position != cameraPositionMouseDown) return;
            
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit; 
            Physics.Raycast(ray, out hit, 100f, layerMask);

            gameObject.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
        }
    }
}
