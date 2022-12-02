using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private TextMeshProUGUI textForMoney;
    [SerializeField] private GameObject scrollViewForShop;
    [SerializeField] private GameObject contentScrollViewForShop;
    [SerializeField] private GameObject prefabBtForPanelModels;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        PlayerData.MoneyChanged.AddListener(ChengedTextForMoney);
        
        for (int i = 0; i < BuildingManager.Instance.objectsForBuilding.Count; i++)
        {
            GameObject bt = Instantiate(prefabBtForPanelModels, contentScrollViewForShop.transform);
            bt.transform.GetChild(0).GetComponent<Image>().sprite = BuildingManager.Instance.objectsForBuilding[i].sprite;
            bt.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = BuildingManager.Instance.objectsForBuilding[i].price + "";
        }
    }

    public void OpenPanelShopMachines()
    {
        scrollViewForShop.SetActive(true);
    }

    public void ClosePanelShopMachines()
    {
        scrollViewForShop.SetActive(false);
    }
    
    
    private void ChengedTextForMoney()
    {
        textForMoney.text = "" + PlayerData.Instanse.GetMoney();
    }
}
