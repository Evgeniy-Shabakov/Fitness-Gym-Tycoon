using BuildingSystem;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ObjectSettings : MonoBehaviour
{
   private Camera _mainCamera;
   private Vector3 _cameraPositionMouseDown;
   
   private LayerMask _layerMaskObjects;
   
   private void Start()
   {
      _mainCamera = Camera.main;
      _layerMaskObjects =  LayerMask.GetMask("Objects");
   }

   void Update()
   {
      if (UIManagerMain.Instance.IsPointerOverUIObject()) return;
      if (Input.touchCount >= 2) return;

      if (Input.GetMouseButtonDown(0)) _cameraPositionMouseDown = _mainCamera.transform.position;
      
      if (Input.GetMouseButtonUp(0))
      {
         if (_mainCamera.transform.position != _cameraPositionMouseDown) return;
            
         Ray ray = new Ray(_mainCamera.ScreenToWorldPoint(Input.mousePosition), _mainCamera.transform.forward);
         RaycastHit hit; 
         
         if (Physics.Raycast(ray, out hit, 100f, _layerMaskObjects) == false) return;
         if (hit.transform.gameObject != gameObject) return;
         
         if (BuildingManager.Instance.objectForBuild != null) return;
         if (_cameraPositionMouseDown != _mainCamera.transform.position) return;
         
         PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
         eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
         List<RaycastResult> results = new List<RaycastResult>();
         EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
         
         if (results.Count > 0) return;
         
         UIManagerPanelObject.Instance.Open(transform.parent.gameObject);
         _cameraPositionMouseDown = new Vector3();
      }
   }

   public void ActivateMoveObject()
   {  
      gameObject.AddComponent<PreBuildingCollision>();
      gameObject.AddComponent<PreBuildingMoving>();

      BuildingManager.Instance.objectForBuild = transform.parent.gameObject;
      transform.parent.transform.SetParent(_mainCamera.transform);
      
      Destroy(gameObject.GetComponent<ObjectSettings>());
   }
}
