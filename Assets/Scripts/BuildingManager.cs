using UnityEngine;
using System.Collections.Generic;


public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instanse;
    
    [SerializeField] private List<ObjectForBuilding> objectsForBuilding;
    [SerializeField] private GameObject prefabHelperBuildingSystem;
    
    [SerializeField] private GameObject scrollViewForShop;
    [SerializeField] private GameObject contentScrollViewForShop;
    [SerializeField] private GameObject prefabBtForPanelModels;
    
    [HideInInspector] public GameObject objectForBuild;
    
    public LayerMask layerMaskForPlane;
    public LayerMask layerMaskForFloor;
    public Material materialForPreview;
    public Material materialForCollision;

    private void Awake()
    {
        Instanse = this;
        
        for (int i = 0; i < objectsForBuilding.Count; i++)
        {
            Instantiate(prefabBtForPanelModels, contentScrollViewForShop.transform);
        }
    }

    public void AcrivatePanelModels()
    {
        scrollViewForShop.SetActive(true);
    }
    
    public void CreateObjectForBuild(int indexOfListModels)
    {
        if (objectForBuild != null) Destroy(objectForBuild);
        
        Vector3 targetPosition;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward*100);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, layerMaskForPlane);
        
        targetPosition = hit.point;
        targetPosition.y = 0f;
        
        objectForBuild = Instantiate(objectsForBuilding[indexOfListModels].prefab, targetPosition, objectsForBuilding[indexOfListModels].prefab.transform.rotation);
        
        objectForBuild.GetComponentInChildren<BoxCollider>().isTrigger = true;
        objectForBuild.transform.SetParent(Camera.main.transform);
        
        scrollViewForShop.SetActive(false);
    }

    public void SetObject()
    {
        if (objectForBuild == null) return;
        if (objectForBuild.GetComponentInChildren<PreBuildingCollision>().PlaceForBuildIsClear() == false) return;
            
        objectForBuild.GetComponentInChildren<BoxCollider>().isTrigger = false;
        objectForBuild.transform.SetParent(null);
        
        Destroy(objectForBuild.GetComponentInChildren<PreBuildingCollision>());
        Destroy(objectForBuild.GetComponentInChildren<PreBuildingMoving>());
        
        objectForBuild.transform.Find(prefabHelperBuildingSystem.name).gameObject.AddComponent<ObjectSettings>();

        if (objectForBuild.transform.Find(prefabHelperBuildingSystem.name).gameObject.GetComponent<ObjectData>().IsNew)
        {
            PlayerData.Instanse.SpendMoney(100);
            objectForBuild.transform.Find(prefabHelperBuildingSystem.name).gameObject.GetComponent<ObjectData>().IsNew = false;
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
