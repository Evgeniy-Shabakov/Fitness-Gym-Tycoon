using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSettings : MonoBehaviour
{
   private Vector3 cameraPositionMouseDown;

   private void OnMouseDown()
   {
      if(EventSystem.current.IsPointerOverGameObject ()) return;
      
      if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
      {
         if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
      }
      
      cameraPositionMouseDown = Camera.main.transform.position;
   }

   private void OnMouseUpAsButton()
   {
      if (BuildingManager.Instanse.objectForBuild != null) return;
      if (cameraPositionMouseDown != Camera.main.transform.position) return;
      
      gameObject.AddComponent<PreBuildingCollision>();
      gameObject.AddComponent<PreBuildingMoving>();

      BuildingManager.Instanse.objectForBuild = transform.parent.gameObject;
      gameObject.GetComponentInChildren<BoxCollider>().isTrigger = true;
      transform.parent.transform.SetParent(Camera.main.transform);
      
      Destroy(gameObject.GetComponent<ObjectSettings>());
   }
}
