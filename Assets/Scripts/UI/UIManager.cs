using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private TextMeshProUGUI textForMoney;
    [SerializeField] private GameObject scrollViewForShop;
    [SerializeField] private GameObject contentScrollViewForShop;
    [SerializeField] private GameObject prefabBtForPanelModels;
    [SerializeField] private GameObject panelHumanClient;
    [SerializeField] private GameObject panelHumanClientGridLayoutGroup;

    [SerializeField] private GameObject clientSpawner;
    [HideInInspector] public GameObject currentGameObjectForPanelHumanClient;
    
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
    
    public bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    
    
    private void ChengedTextForMoney()
    {
        textForMoney.text = "" + PlayerData.Instanse.GetMoney();
    }

    public void ClosePanelHumanClient()
    {
        foreach(Transform child in panelHumanClientGridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
        
        panelHumanClient.SetActive(false);
    }

    public void BtEyePressed()
    {
        CameraController.Instance.objectForFollow = currentGameObjectForPanelHumanClient;
    }

    public void BtNextClientPressed()
    {
        int i = currentGameObjectForPanelHumanClient.transform.GetSiblingIndex();

        if (i < clientSpawner.transform.childCount - 1) i++;
        else i = 0;
        
        currentGameObjectForPanelHumanClient = clientSpawner.transform.GetChild(i).gameObject;
        currentGameObjectForPanelHumanClient.GetComponent<PanelHumanClient>().OnMouseUpAsButton();
    }
    
    public void BtPreviousClientPressed()
    {
        int i = currentGameObjectForPanelHumanClient.transform.GetSiblingIndex();

        if (i == 0) i = clientSpawner.transform.childCount - 1;
        else i--;
        
        currentGameObjectForPanelHumanClient = clientSpawner.transform.GetChild(i).gameObject;
        currentGameObjectForPanelHumanClient.GetComponent<PanelHumanClient>().OnMouseUpAsButton();
    }
}
