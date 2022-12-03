using UnityEngine;


public class ObjectSettings : MonoBehaviour
{
   private Vector3 cameraPositionMouseDown;

   private void OnMouseDown()
   {
      if (UIManager.Instance.IsPointerOverUIObject()) return;

      cameraPositionMouseDown = Camera.main.transform.position;
   }

   private void OnMouseUpAsButton()
   {
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
