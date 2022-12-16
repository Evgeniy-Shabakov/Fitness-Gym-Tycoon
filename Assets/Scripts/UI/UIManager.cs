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
    [SerializeField] private GameObject panelHumanClientTargets;
    [SerializeField] private Image imageSmile;
    [SerializeField] private Slider sliderMood;
    [SerializeField] private Image fillSliderMood;
    [SerializeField] private GameObject prefabImageTargetPanelHumanClient;
    [SerializeField] private Sprite spriteStatusTrue;
    [SerializeField] private Sprite spriteStatusFalse;
    [SerializeField] private Sprite spriteSmileHappy;
    [SerializeField] private Sprite spriteSmileMiddle;
    [SerializeField] private Sprite spriteSmileSad;
    [SerializeField] private Color colorHappy;
    [SerializeField] private Color colorMiddle;
    [SerializeField] private Color colorSad;
    
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
        foreach(Transform child in panelHumanClientTargets.transform)
        {
            Destroy(child.gameObject);
        }
        
        panelHumanClient.SetActive(false);
        currentGameObjectForPanelHumanClient = null;
    }

    public void OpenAndFillPanelHumanClient(GameObject currentClient)
    {
        if (panelHumanClient.activeSelf) ClosePanelHumanClient();
        panelHumanClient.SetActive(true);
        
        currentGameObjectForPanelHumanClient = currentClient;
        CameraController.Instance.objectForFollow = currentGameObjectForPanelHumanClient;
        
        HumanControls humanControls = currentGameObjectForPanelHumanClient.GetComponent<HumanControls>();
        
        for (int i = 0; i < humanControls.countTargets; i++)
        {
            GameObject image = Instantiate(prefabImageTargetPanelHumanClient, panelHumanClientTargets.transform);
            
            int j = humanControls.targetsArray[i];
            image.transform.GetChild(0).GetComponent<Image>().sprite = BuildingManager.Instance.objectsForBuilding[j].sprite;
        }

        Invoke("UpdateDataPanelHumanClient", Time.deltaTime);
    }

    public void UpdateDataPanelHumanClient()
    {
        HumanControls humanControls = currentGameObjectForPanelHumanClient.GetComponent<HumanControls>();
            
        for (int i = 0; i < humanControls.countTargets; i++)
        {   
            GameObject image = panelHumanClientTargets.transform.GetChild(i).gameObject;
            
            if (i < humanControls.indexInTargetsArray)
            {   
                if (humanControls.targetsStatus[i] == true)
                {
                    image.transform.GetChild(1).GetComponent<Image>().sprite = spriteStatusTrue;
                }

                else
                {
                    image.transform.GetChild(1).GetComponent<Image>().sprite = spriteStatusFalse;
                }
            }
        }
        
        sliderMood.value = humanControls.GetMood();
        
        if (sliderMood.value <= HumanControls.moodSad)
        {
            imageSmile.sprite = spriteSmileSad;
            fillSliderMood.color = colorSad;
        }
        else if (sliderMood.value > HumanControls.moodSad && sliderMood.value < HumanControls.moodHappy)
        {
            imageSmile.sprite = spriteSmileMiddle;
            fillSliderMood.color = colorMiddle;
        }
        else
        {
            imageSmile.sprite = spriteSmileHappy;
            fillSliderMood.color = colorHappy;
        }
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
        OpenAndFillPanelHumanClient(currentGameObjectForPanelHumanClient);
    }
    
    public void BtPreviousClientPressed()
    {
        int i = currentGameObjectForPanelHumanClient.transform.GetSiblingIndex();

        if (i == 0) i = clientSpawner.transform.childCount - 1;
        else i--;
        
        currentGameObjectForPanelHumanClient = clientSpawner.transform.GetChild(i).gameObject;
        OpenAndFillPanelHumanClient(currentGameObjectForPanelHumanClient);
    }
}
