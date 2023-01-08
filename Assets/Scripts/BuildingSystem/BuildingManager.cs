using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance;
    
        public List<ObjectForBuilding> objectsForBuilding;
        [SerializeField] private GameObject prefabHelperBuildingSystem;
        [SerializeField] private GameObject parentForAllDynamicObjects;
    
        [HideInInspector] public GameObject objectForBuild;
        private GameObject _childHelperObjectForBuild;
    
        public LayerMask layerMaskForPlane;
        public Material materialForPreview;
        public Material materialForCollision;

        private Camera _mainCamera;
    
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        public void CreateObjectForBuild(int indexOfListModels)
        {
            if (objectForBuild != null) Destroy(objectForBuild);
        
            Vector3 targetPosition;

            Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward*100);
            RaycastHit hit; 
            Physics.Raycast(ray, out hit, 100f, layerMaskForPlane);
        
            targetPosition = hit.point;
            targetPosition.y = objectsForBuilding[indexOfListModels].prefab.transform.position.y;
        
            objectForBuild = Instantiate(objectsForBuilding[indexOfListModels].prefab, targetPosition, objectsForBuilding[indexOfListModels].prefab.transform.rotation);
        
            objectForBuild.transform.SetParent(_mainCamera.transform);

            objectForBuild.GetComponentInChildren<ObjectData>().isNew = true;
            objectForBuild.GetComponentInChildren<ObjectData>().indexInBuildingManagerList = indexOfListModels;
            objectForBuild.GetComponentInChildren<ObjectData>().price = objectsForBuilding[indexOfListModels].price;
        
            UIManagerPanelGameShop.Instance.Close();
        }

        public void SetObject()
        {
            _childHelperObjectForBuild = objectForBuild.transform.Find(prefabHelperBuildingSystem.name).gameObject;
        
            if (_childHelperObjectForBuild.GetComponent<PreBuildingCollision>().GetInstallationAllowed() == false) return;
        
            objectForBuild.transform.SetParent(parentForAllDynamicObjects.transform);
        
            Destroy(objectForBuild.GetComponentInChildren<PreBuildingCollision>());
            Destroy(objectForBuild.GetComponentInChildren<PreBuildingMoving>());
            _childHelperObjectForBuild.AddComponent<ObjectSettings>();
        
            objectForBuild = null;
            SaveLoadManager.Instance.Save();
        }

        public void CreateAndSetObjectForLoad(int indexOfListModels, Vector3 pos, Quaternion rotation)
        {
            objectForBuild = Instantiate(objectsForBuilding[indexOfListModels].prefab, pos, rotation);
        
            objectForBuild.transform.SetParent(parentForAllDynamicObjects.transform);
        
            objectForBuild.GetComponentInChildren<ObjectData>().isNew = false;
            objectForBuild.GetComponentInChildren<ObjectData>().indexInBuildingManagerList = indexOfListModels;
            objectForBuild.GetComponentInChildren<ObjectData>().price = objectsForBuilding[indexOfListModels].price;
        
            _childHelperObjectForBuild = objectForBuild.transform.Find(prefabHelperBuildingSystem.name).gameObject;
        
            Destroy(objectForBuild.GetComponentInChildren<PreBuildingCollision>());
            Destroy(objectForBuild.GetComponentInChildren<PreBuildingMoving>());
            _childHelperObjectForBuild.AddComponent<ObjectSettings>();
        
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
}
