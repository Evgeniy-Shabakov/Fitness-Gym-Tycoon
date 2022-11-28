using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabObjForBuild;
    [SerializeField] private LayerMask layerMask;
    
    private GameObject objectForBuild;

    public void CreateObjectForBuild()
    {
        if (objectForBuild != null) Destroy(objectForBuild);
        
        Vector3 targetPosition;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward*100);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, layerMask);
        
        targetPosition = hit.point;
        targetPosition.y = 0f;
        
        objectForBuild = Instantiate(prefabObjForBuild, targetPosition, Quaternion.identity);
        
        objectForBuild.GetComponent<BoxCollider>().isTrigger = true;
        objectForBuild.transform.SetParent(Camera.main.transform);
    }

    public void SetObject()
    {
        if (objectForBuild == null) return;
        if (objectForBuild.GetComponent<PreBuildingCollision>().PlaceForBuilIsClear() == false) return;
            
        objectForBuild.GetComponent<BoxCollider>().isTrigger = false;
        objectForBuild.transform.SetParent(null);
        
        Destroy(objectForBuild.GetComponent<PreBuildingCollision>());
        Destroy(objectForBuild.GetComponent<PreBuildingMoving>());
        objectForBuild = null;
    }

    public void RotateObject()
    {
        objectForBuild.transform.Rotate(0, 45, 0);
    }

    public void DeleteObject()
    {
        Destroy(objectForBuild);
    }
}
