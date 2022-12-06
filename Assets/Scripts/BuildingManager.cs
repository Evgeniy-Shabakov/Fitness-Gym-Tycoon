using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;
    
    public List<ObjectForBuilding> objectsForBuilding;
    [SerializeField] private GameObject prefabHelperBuildingSystem;
    [SerializeField] private GameObject parentForAllDynamicObjects;
    
    [HideInInspector] public GameObject objectForBuild;
    private GameObject childHelperObjectForBuild;
    
    public LayerMask layerMaskForPlane;
    public LayerMask layerMaskForFloor;
    public Material materialForPreview;
    public Material materialForCollision;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateObjectForBuild(int indexOfListModels)
    {
        if (objectForBuild != null) Destroy(objectForBuild);
        
        Vector3 targetPosition;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward*100);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, layerMaskForPlane);
        
        targetPosition = hit.point;
        targetPosition.y = objectsForBuilding[indexOfListModels].prefab.transform.position.y;
        
        objectForBuild = Instantiate(objectsForBuilding[indexOfListModels].prefab, targetPosition, objectsForBuilding[indexOfListModels].prefab.transform.rotation);
        
        objectForBuild.GetComponentInChildren<BoxCollider>().isTrigger = true;
        objectForBuild.transform.SetParent(Camera.main.transform);

        objectForBuild.GetComponentInChildren<ObjectData>().indexInBuildingManagerList = indexOfListModels;
        
        UIManager.Instance.ClosePanelShopMachines();
    }

    public void SetObject()
    {
        if (objectForBuild == null) return;

        childHelperObjectForBuild = objectForBuild.transform.Find(prefabHelperBuildingSystem.name).gameObject;
        
        if (childHelperObjectForBuild.GetComponent<PreBuildingCollision>().PlaceForBuildIsClear() == false) return;
        
        objectForBuild.transform.SetParent(parentForAllDynamicObjects.transform);
        
        childHelperObjectForBuild.GetComponent<BoxCollider>().isTrigger = false;
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
