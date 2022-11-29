using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSettings : MonoBehaviour
{
   private void OnMouseUp()
   {
      gameObject.AddComponent<PreBuildingCollision>();
      gameObject.AddComponent<PreBuildingMoving>();

      BuildingManager.Instanse.objectForBuild = transform.parent.gameObject;
      transform.parent.transform.SetParent(Camera.main.transform);
      
      Destroy(gameObject.GetComponent<ObjectSettings>());
   }
}
