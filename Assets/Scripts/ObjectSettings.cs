using UnityEngine;


public class ObjectSettings : MonoBehaviour
{
   private Vector3 cameraPositionMouseDown;
   
   private LayerMask layerMaskObjects;
   
   private void Start()
   {
      layerMaskObjects =  LayerMask.GetMask("Objects");
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
         
         if (Physics.Raycast(ray, out hit, 100f, layerMaskObjects) == false) return;
         if (hit.transform.gameObject != gameObject) return;
         
         if (BuildingManager.Instance.objectForBuild != null) return;
         if (cameraPositionMouseDown != Camera.main.transform.position) return;
      
         gameObject.AddComponent<PreBuildingCollision>();
         gameObject.AddComponent<PreBuildingMoving>();

         BuildingManager.Instance.objectForBuild = transform.parent.gameObject;
         gameObject.GetComponentInChildren<BoxCollider>().isTrigger = true;
         transform.parent.transform.SetParent(Camera.main.transform);
      
         Destroy(gameObject.GetComponent<ObjectSettings>());
      }
   }
}
