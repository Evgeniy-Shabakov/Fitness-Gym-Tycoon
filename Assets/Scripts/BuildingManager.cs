using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabObjForBuild;

    public void CreateObjectForBuild()
    {
        Instantiate(prefabObjForBuild, Vector3.zero, Quaternion.identity);
    }
}
