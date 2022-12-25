using UI;
using UnityEngine;

public class PreBuildingMoving : MonoBehaviour
{
    private Camera mainCamera;
    
    private bool cameraPositionIsChanged;
    private Vector3 cameraPositionMouseDown;

    private Vector3 touch;
    
    private GameObject parent;
    private LayerMask layerMaskObjects;

    private bool objectIsСaptured;
    

    private void Start()
    {
        mainCamera = Camera.main;
        
        parent = transform.parent.gameObject;
        layerMaskObjects =  LayerMask.GetMask("Objects");
        objectIsСaptured = false;
    }
    
    void Update()
    {
        if (UIManagerMain.Instance.IsPointerOverUIObject()) return;
        if (Input.touchCount >= 2) return;

        if (Input.GetMouseButtonDown(0))
        {
            cameraPositionMouseDown = mainCamera.transform.position;
            
            Ray ray = new Ray(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100f, layerMaskObjects) == false) return;
            if (hit.transform.gameObject != gameObject) return;
            
            mainCamera.gameObject.GetComponent<CameraController>().enabled = false;
            objectIsСaptured = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mainCamera.gameObject.GetComponent<CameraController>().enabled = true;
            objectIsСaptured = false;
            
            if (mainCamera.transform.position != cameraPositionMouseDown) return;
            
            Ray ray = new Ray(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.forward);
            RaycastHit hit; 
            Physics.Raycast(ray, out hit, 100f, BuildingManager.Instance.layerMaskForPlane);

            parent.transform.position = new Vector3(hit.point.x, parent.transform.position.y, hit.point.z);
        }

        if (Input.GetMouseButton(0))
        {
            if (objectIsСaptured == false) return;
            
            Ray ray2 = new Ray(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.forward);
            RaycastHit hit2; 
            Physics.Raycast(ray2, out hit2, 100f, BuildingManager.Instance.layerMaskForPlane);

            parent.transform.position = new Vector3(hit2.point.x, parent.transform.position.y, hit2.point.z);
        }
    }
}