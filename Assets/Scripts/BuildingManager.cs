using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabObjForBuild;
    [SerializeField] private Camera mainCamera;

    public void CreateObjectForBuild()
    {
        Vector3 targetPosition = new Vector3();
        Vector3 rayDirection = new Vector3(Screen.width/2, Screen.height/2, 0);
        
        Ray ray = mainCamera.ScreenPointToRay(rayDirection);
        
        RaycastHit hit; 
        Physics.Raycast(ray, out hit);

        targetPosition = hit.point;
        targetPosition.y = 0.5f;
        
        Instantiate(prefabObjForBuild, targetPosition, Quaternion.identity);
    }
}
