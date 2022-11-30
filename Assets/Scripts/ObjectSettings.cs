using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ObjectSettings : MonoBehaviour
{
   private Vector3 cameraPositionMouseDown;

   private void OnMouseDown()
   {
      if (IsPointerOverUIObject()) return;

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
   
   private bool IsPointerOverUIObject() {
      PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
      eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
      List<RaycastResult> results = new List<RaycastResult>();
      EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
      return results.Count > 0;
   }
}
