using UnityEngine;

public class BtPrefabForBuyModel : MonoBehaviour
{
    public void btPrfabForBuyModelPressed()
    {
        UIManager.Instance.OpenPanelBuildObject(transform.GetSiblingIndex());
        BuildingManager.Instance.CreateObjectForBuild(transform.GetSiblingIndex());
    }
}
