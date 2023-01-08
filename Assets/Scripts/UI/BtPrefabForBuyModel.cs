using BuildingSystem;
using UnityEngine;

public class BtPrefabForBuyModel : MonoBehaviour
{
    public void btPrfabForBuyModelPressed()
    {
        BuildingManager.Instance.CreateObjectForBuild(transform.GetSiblingIndex());
        UIManagerPanelObject.Instance.Open(BuildingManager.Instance.objectForBuild);
    }
}
