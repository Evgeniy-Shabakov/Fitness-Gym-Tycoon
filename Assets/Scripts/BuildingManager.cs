using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instanse;
    
    [SerializeField] private GameObject prefabObjForBuild;
    [SerializeField] private GameObject prefabChildForBuild;
    
    [HideInInspector] public GameObject objectForBuild;
    
    public LayerMask layerMaskForBuilddAllowed;
    public Material materialForPreview;
    public Material materialForCollision;

    private void Awake()
    {
        Instanse = this;
    }

    public void CreateObjectForBuild()
    {
        if (objectForBuild != null) Destroy(objectForBuild);
        
        Vector3 targetPosition;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward*100);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, layerMaskForBuilddAllowed);
        
        targetPosition = hit.point;
        targetPosition.y = 0f;
        
        objectForBuild = Instantiate(prefabObjForBuild, targetPosition, prefabObjForBuild.transform.rotation);
        
        objectForBuild.GetComponentInChildren<BoxCollider>().isTrigger = true;
        objectForBuild.transform.SetParent(Camera.main.transform);
    }

    public void SetObject()
    {
        if (objectForBuild == null) return;
        if (objectForBuild.GetComponentInChildren<PreBuildingCollision>().PlaceForBuildIsClear() == false) return;
            
        objectForBuild.GetComponentInChildren<BoxCollider>().isTrigger = false;
        objectForBuild.transform.SetParent(null);
        
        Destroy(objectForBuild.GetComponentInChildren<PreBuildingCollision>());
        Destroy(objectForBuild.GetComponentInChildren<PreBuildingMoving>());
        
        objectForBuild.transform.Find(prefabChildForBuild.name).gameObject.AddComponent<ObjectSettings>();

        if (objectForBuild.transform.Find(prefabChildForBuild.name).gameObject.GetComponent<ObjectData>().IsNew)
        {
            PlayerData.Instanse.SpendMoney(100);
            objectForBuild.transform.Find(prefabChildForBuild.name).gameObject.GetComponent<ObjectData>().IsNew = false;
        }
        
        objectForBuild = null;
    }

    public void RotateObject()
    {
        if (objectForBuild == null) return;
        
        objectForBuild.transform.Rotate(0, 45, 0);
    }

    public void DeleteObject()
    {
        Destroy(objectForBuild);
    }
}
