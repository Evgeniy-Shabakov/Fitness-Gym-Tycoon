using UnityEngine;

public class PreBuildingMoving : MonoBehaviour
{
    private bool cameraPositionIsChanged;
    private Vector3 cameraPositionMouseDown;

    private Vector3 touch;
    
    private GameObject parent;
    private LayerMask layerMaskObjects;

    private bool objectIsСaptured;
    

    private void Start()
    {
        parent = transform.parent.gameObject;
        layerMaskObjects =  LayerMask.GetMask("Objects");
        objectIsСaptured = false;
    }
    

    /*private void OnMouseDrag()
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
    }*/

    void Update()
    {
        if (UIManager.Instance.IsPointerOverUIObject()) return;
        if (Input.touchCount >= 2) return;

        if (Input.GetMouseButtonDown(0))
        {
            cameraPositionMouseDown = Camera.main.transform.position;
            
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100f, layerMaskObjects) == false) return;
            if (hit.transform.gameObject != gameObject) return;
            
            Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
            objectIsСaptured = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Camera.main.gameObject.GetComponent<CameraController>().enabled = true;
            objectIsСaptured = false;
            
            if (Camera.main.transform.position != cameraPositionMouseDown) return;
            
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit; 
            Physics.Raycast(ray, out hit, 100f, BuildingManager.Instance.layerMaskForPlane);

            parent.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }

        if (Input.GetMouseButton(0))
        {
            if (objectIsСaptured == false) return;
            
            Ray ray2 = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit2; 
            Physics.Raycast(ray2, out hit2, 100f, BuildingManager.Instance.layerMaskForPlane);

            parent.transform.position = new Vector3(hit2.point.x, 0f, hit2.point.z);
        }
    }
}