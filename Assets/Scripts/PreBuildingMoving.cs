using UnityEngine;
using UnityEngine.EventSystems;

public class PreBuildingMoving : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private bool cameraPositionIsChanged;
    private Vector3 cameraPositionMouseDown;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cameraPositionMouseDown = Camera.main.transform.position;
        }
        
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Camera.main.transform.position != cameraPositionMouseDown) return;
                
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit; 
            Physics.Raycast(ray, out hit, 100f, layerMask);

            gameObject.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
        }
    }
}
