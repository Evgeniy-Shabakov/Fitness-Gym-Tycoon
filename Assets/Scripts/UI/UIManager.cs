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
    
    [SerializeField] private GameObject panelBuildObject;
    [SerializeField] private Image spriteBuildObject;
    [SerializeField] private TextMeshProUGUI textPriceBuildObject;
    [SerializeField] private TextMeshProUGUI textDescriptionBuildObject;
    [SerializeField] private GameObject btRotate;
    [SerializeField] private GameObject btSet;
    [SerializeField] private GameObject btActivateMove;
    [SerializeField] private GameObject btSell;
    [SerializeField] private GameObject currentGameObjectForBuildPanel;
    
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
        CloseAllPanels();
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

    public bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void ClosePanelBuildObject()
    {
        if (currentGameObjectForBuildPanel.GetComponentInChildren<ObjectData>().isNew == false 
            && BuildingManager.Instance.objectForBuild != null)
        {
            ObjectData objectData = currentGameObjectForBuildPanel.GetComponentInChildren<ObjectData>();

            currentGameObjectForBuildPanel.transform.position = objectData.positionBeforeMove;
            currentGameObjectForBuildPanel.transform.rotation = objectData.rotationBeforeMove;
            
            currentGameObjectForBuildPanel.GetComponentInChildren<PreBuildingCollision>().SetPlaceForBuildIsClear(true);
            BuildingManager.Instance.SetObject();
        }
        
        currentGameObjectForBuildPanel = null;
        
        BuildingManager.Instance.DeleteObject();
        panelBuildObject.SetActive(false);
    }
    
    public void OpenPanelBuildObject(GameObject current)
    {
        CloseAllPanels();
        
        currentGameObjectForBuildPanel = current;
            
        panelBuildObject.SetActive(true);

        ObjectData objectData = current.GetComponentInChildren<ObjectData>();
        int i = objectData.indexInBuildingManagerList;

        if (objectData.isNew)
        {
            btRotate.SetActive(true);
            btSet.SetActive(true);
            btActivateMove.SetActive(false);
            btSell.SetActive(false);
        }
        else
        {
            btRotate.SetActive(false);
            btSet.SetActive(false);
            btActivateMove.SetActive(true);
            btSell.SetActive(true);
        }
            
        spriteBuildObject.sprite = BuildingManager.Instance.objectsForBuilding[i].sprite;
        textPriceBuildObject.text = BuildingManager.Instance.objectsForBuilding[i].price + "";
        textDescriptionBuildObject.text = BuildingManager.Instance.objectsForBuilding[i].description;
    }

    public void BtSetObjectPressed()
    {
        BuildingManager.Instance.SetObject();
        if (BuildingManager.Instance.objectForBuild == null)
        {
            panelBuildObject.SetActive(false);
        }
    }

    public void BtSellObject()
    {
        Destroy(currentGameObjectForBuildPanel);
        ClosePanelBuildObject();
    }

    public void BtActivateMoveObject()
    {
        btRotate.SetActive(true);
        btSet.SetActive(true);
        btActivateMove.SetActive(false);
        btSell.SetActive(false);
        
        currentGameObjectForBuildPanel.GetComponentInChildren<ObjectSettings>().ActivateMoveObject();
        
        ObjectData objectData = currentGameObjectForBuildPanel.GetComponentInChildren<ObjectData>();
        
        objectData.positionBeforeMove = currentGameObjectForBuildPanel.transform.position;
        objectData.rotationBeforeMove = currentGameObjectForBuildPanel.transform.rotation;
    }

    public void CloseAllPanels()
    {
        if (panelBuildObject.activeSelf)
        {
            ClosePanelBuildObject();
        }
        if (UIManagerPanelHumanClient.Instance.panelHumanClient.activeSelf)
        {
            UIManagerPanelHumanClient.Instance.Close();
        }

        if (scrollViewForShop.activeSelf)
        {
            ClosePanelShopMachines();
        }
    }
}
