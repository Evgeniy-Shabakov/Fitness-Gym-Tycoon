using UnityEngine;

public class PreBuildingMoving : MonoBehaviour
{
    private bool cameraPositionIsChanged;
    private Vector3 cameraPositionMouseDown;

    private Vector3 touch;
    
    private GameObject parent;

    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    

    private void OnMouseDrag()
    {
        Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
        
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, BuildingManager.Instance.layerMaskForPlane);

        parent.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
    }

    private void OnMouseUp()
    {
        Camera.main.gameObject.GetComponent<CameraController>().enabled = true;
    }

    void Update()
    {
        if (UIManager.Instance.IsPointerOverUIObject()) return;
        if (Input.touchCount >= 2) return;
        
        if (Input.GetMouseButtonDown(0)) cameraPositionMouseDown = Camera.main.transform.position;

        if (Input.GetMouseButtonUp(0))
        {
            if (Camera.main.transform.position != cameraPositionMouseDown) return;
            
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit; 
            Physics.Raycast(ray, out hit, 100f, BuildingManager.Instance.layerMaskForPlane);

            parent.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
    }
}