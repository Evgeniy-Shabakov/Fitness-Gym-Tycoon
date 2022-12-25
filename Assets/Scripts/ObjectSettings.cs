using BuildingSystem;
using UI;
using UnityEngine;


public class ObjectSettings : MonoBehaviour
{
   private Camera mainCamera;
   private Vector3 cameraPositionMouseDown;
   
   private LayerMask layerMaskObjects;
   
   private void Start()
   {
      mainCamera = Camera.main;
      layerMaskObjects =  LayerMask.GetMask("Objects");
   }
   
   void Update()
   {
      if (UIManagerMain.Instance.IsPointerOverUIObject()) return;
      if (Input.touchCount >= 2) return;
        
      if (Input.GetMouseButtonDown(0)) cameraPositionMouseDown = mainCamera.transform.position;

      if (Input.GetMouseButtonUp(0))
      {
         if (mainCamera.transform.position != cameraPositionMouseDown) return;
            
         Ray ray = new Ray(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.forward);
         RaycastHit hit; 
         
         if (Physics.Raycast(ray, out hit, 100f, layerMaskObjects) == false) return;
         if (hit.transform.gameObject != gameObject) return;
         
         if (BuildingManager.Instance.objectForBuild != null) return;
         if (cameraPositionMouseDown != mainCamera.transform.position) return;

         UIManagerPanelObject.Instance.Open(transform.parent.gameObject);
      }
   }

   public void ActivateMoveObject()
   {  
      gameObject.AddComponent<PreBuildingCollision>();
      gameObject.AddComponent<PreBuildingMoving>();

      BuildingManager.Instance.objectForBuild = transform.parent.gameObject;
      transform.parent.transform.SetParent(mainCamera.transform);
      
      Destroy(gameObject.GetComponent<ObjectSettings>());
   }
}
