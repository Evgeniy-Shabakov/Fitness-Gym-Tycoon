using UnityEngine;
using System.Collections.Generic;


public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instanse;
    
    [SerializeField] private List<GameObject> prefabsObjectsForBuild;
    [SerializeField] private GameObject prefabHelperBuildingSystem;
    
    [SerializeField] private GameObject panelModels;
    [SerializeField] private GameObject prefabBtForPanelModels;
    
    [HideInInspector] public GameObject objectForBuild;
    
    public LayerMask layerMaskForBuilddAllowed;
    public Material materialForPreview;
    public Material materialForCollision;

    private void Awake()
    {
        Instanse = this;
        
        for (int i = 0; i < prefabsObjectsForBuild.Count; i++)
        {
            Instantiate(prefabBtForPanelModels, panelModels.transform);
        }
    }

    public void AcrivatePanelModels()
    {
        panelModels.SetActive(true);
    }
    
    public void CreateObjectForBuild(int indexOfListModels)
    {
        if (objectForBuild != null) Destroy(objectForBuild);
        
        Vector3 targetPosition;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward*100);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit, 100f, layerMaskForBuilddAllowed);
        
        targetPosition = hit.point;
        targetPosition.y = 0f;
        
        objectForBuild = Instantiate(prefabsObjectsForBuild[indexOfListModels], targetPosition, prefabsObjectsForBuild[indexOfListModels].transform.rotation);
        
        objectForBuild.GetComponentInChildren<BoxCollider>().isTrigger = true;
        objectForBuild.transform.SetParent(Camera.main.transform);
        
        panelModels.SetActive(false);
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
