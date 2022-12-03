using UnityEngine;

public class BtPrefabForBuyModel : MonoBehaviour
{
    private GameObject buildingManager;
    private BuildingManager bm;
    
    void Start()
    {
        bm = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    public void btPrfabForBuyModelPressed()
    {
        bm.CreateObjectForBuild(transform.GetSiblingIndex());
    }
}
