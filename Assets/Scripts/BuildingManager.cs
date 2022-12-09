using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;
    
    public static UnityEvent ObjectInstalledOrDeleted = new UnityEvent();
    
    public List<ObjectForBuilding> objectsForBuilding;
    [SerializeField] private GameObject prefabHelperBuildingSystem;
    [SerializeField] private GameObject parentForAllDynamicObjects;
    
    [HideInInspector] public GameObject objectForBuild;
    private GameObject childHelperObjectForBuild;
    
    public LayerMask layerMaskForPlane;
    public LayerMask layerMaskForFloor;
    public Material materialForPreview;
    public Material materialForCollision;

    private Camera mainCamera;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void CreateObjectForBuild(int indexOfListModels)
    {
        if (objectForBuild != null) Destroy(objectForBuild);
        
        Vector3 targetPosition;

        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward*100);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, layerMaskForPlane);
        
        targetPosition = hit.point;
        targetPosition.y = objectsForBuilding[indexOfListModels].prefab.transform.position.y;
        
        objectForBuild = Instantiate(objectsForBuilding[indexOfListModels].prefab, targetPosition, objectsForBuilding[indexOfListModels].prefab.transform.rotation);
        
        objectForBuild.transform.SetParent(mainCamera.transform);

        objectForBuild.GetComponentInChildren<ObjectData>().isNew = true;
        objectForBuild.GetComponentInChildren<ObjectData>().indexInBuildingManagerList = indexOfListModels;
        
        UIManager.Instance.ClosePanelShopMachines();
    }

    public void SetObject()
    {
        if (objectForBuild == null) return;

        childHelperObjectForBuild = objectForBuild.transform.Find(prefabHelperBuildingSystem.name).gameObject;
        
        if (childHelperObjectForBuild.GetComponent<PreBuildingCollision>().PlaceForBuildIsClear() == false) return;
        
        objectForBuild.transform.SetParent(parentForAllDynamicObjects.transform);
        
        Destroy(objectForBuild.GetComponentInChildren<PreBuildingCollision>());
        Destroy(objectForBuild.GetComponentInChildren<PreBuildingMoving>());
        childHelperObjectForBuild.AddComponent<ObjectSettings>();

        ObjectData currentObjectData = childHelperObjectForBuild.GetComponent<ObjectData>();
        
        if (currentObjectData.isNew)
        {
            int price = objectsForBuilding[currentObjectData.indexInBuildingManagerList].price;
            
            PlayerData.Instanse.SpendMoney(price);
            childHelperObjectForBuild.GetComponent<ObjectData>().isNew = false;
        }

        objectForBuild = null;
        ObjectInstalledOrDeleted.Invoke();
    }

    public void CreateAndSetObjectForLoad(int indexOfListModels, Vector3 pos, Quaternion rotation)
    {
        objectForBuild = Instantiate(objectsForBuilding[indexOfListModels].prefab, pos, rotation);
        
        objectForBuild.transform.SetParent(parentForAllDynamicObjects.transform);
        
        objectForBuild.GetComponentInChildren<ObjectData>().isNew = false;
        objectForBuild.GetComponentInChildren<ObjectData>().indexInBuildingManagerList = indexOfListModels;
        
        childHelperObjectForBuild = objectForBuild.transform.Find(prefabHelperBuildingSystem.name).gameObject;
        
        Destroy(objectForBuild.GetComponentInChildren<PreBuildingCollision>());
        Destroy(objectForBuild.GetComponentInChildren<PreBuildingMoving>());
        childHelperObjectForBuild.AddComponent<ObjectSettings>();
        
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
        
        ObjectInstalledOrDeleted.Invoke();
    }
}
