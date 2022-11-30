using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSettings : MonoBehaviour
{
   private void OnMouseUp()
   {
      if (BuildingManager.Instanse.objectForBuild != null) return;
      
      gameObject.AddComponent<PreBuildingCollision>();
      gameObject.AddComponent<PreBuildingMoving>();

      BuildingManager.Instanse.objectForBuild = transform.parent.gameObject;
      gameObject.GetComponentInChildren<BoxCollider>().isTrigger = true;
      transform.parent.transform.SetParent(Camera.main.transform);
      
      Destroy(gameObject.GetComponent<ObjectSettings>());
   }
}
