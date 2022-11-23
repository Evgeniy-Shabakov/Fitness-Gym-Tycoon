using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabObjForBuild;
    [SerializeField] private Camera mainCamera;

    private GameObject objectForBuild;

    public void CreateObjectForBuild()
    {
        if (objectForBuild != null) Destroy(objectForBuild);
        
        Vector3 targetPosition = new Vector3();
        Vector3 rayDirection = new Vector3(Screen.width/2, Screen.height/2, 0);
        
        Ray ray = mainCamera.ScreenPointToRay(rayDirection);
        
        RaycastHit hit; 
        Physics.Raycast(ray, out hit);

        targetPosition = hit.point;
        targetPosition.y = 0.5f;
        
        objectForBuild = Instantiate(prefabObjForBuild, targetPosition, Quaternion.identity);
        
        objectForBuild.GetComponent<BoxCollider>().isTrigger = true;
        objectForBuild.transform.SetParent(mainCamera.transform);
    }

    public void SetObject()
    {
        if (objectForBuild == null) return;
        if (objectForBuild.GetComponent<ObjectForBuild>().PlaceForBuilIsClear() == false) return;
            
        objectForBuild.GetComponent<BoxCollider>().isTrigger = false;
        objectForBuild.transform.SetParent(null);
        
        Destroy(objectForBuild.GetComponent<ObjectForBuild>());
        objectForBuild = null;
    }

    public void DeleteObject()
    {
        Destroy(objectForBuild);
    }
}
