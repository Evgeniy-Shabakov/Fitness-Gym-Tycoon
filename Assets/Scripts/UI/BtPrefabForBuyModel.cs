using UnityEngine;

public class BtPrefabForBuyModel : MonoBehaviour
{
    public void btPrfabForBuyModelPressed()
    {
        BuildingManager.Instance.CreateObjectForBuild(transform.GetSiblingIndex());
        UIManager.Instance.OpenPanelBuildObject(BuildingManager.Instance.objectForBuild);
    }
}
