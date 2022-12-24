using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerPanelObject : MonoBehaviour
{
    public static UIManagerPanelObject Instance;

    public GameObject panelObject;
    
    [SerializeField] private Image spriteObject;
    [SerializeField] private TextMeshProUGUI textPriceObject;
    [SerializeField] private TextMeshProUGUI textDescriptionObject;
    
    [SerializeField] private GameObject btRotate;
    [SerializeField] private GameObject btSet;
    [SerializeField] private GameObject btActivateMove;
    [SerializeField] private GameObject btSell;
    
    private GameObject currentGameObjectForPanel;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void Close()
    {
        if (currentGameObjectForPanel.GetComponentInChildren<ObjectData>().isNew == false 
            && BuildingManager.Instance.objectForBuild != null)
        {
            ObjectData objectData = currentGameObjectForPanel.GetComponentInChildren<ObjectData>();

            currentGameObjectForPanel.transform.position = objectData.positionBeforeMove;
            currentGameObjectForPanel.transform.rotation = objectData.rotationBeforeMove;
            
            currentGameObjectForPanel.GetComponentInChildren<PreBuildingCollision>().SetPlaceForBuildIsClear(true);
            BuildingManager.Instance.SetObject();
        }
        
        currentGameObjectForPanel = null;
        
        BuildingManager.Instance.DeleteObject();
        panelObject.SetActive(false);
    }
    
    public void Open(GameObject current)
    {
        UIManagerMain.Instance.CloseAllPanels();
        
        currentGameObjectForPanel = current;
            
        panelObject.SetActive(true);

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
            
        spriteObject.sprite = BuildingManager.Instance.objectsForBuilding[i].sprite;
        textPriceObject.text = objectData.price + "";
        textDescriptionObject.text = BuildingManager.Instance.objectsForBuilding[i].description;
    }

    public void BtSetPressed()
    {
        ObjectData objectData = currentGameObjectForPanel.GetComponentInChildren<ObjectData>();
        
        if (objectData.isNew)
        {
            int price = BuildingManager.Instance.objectsForBuilding[objectData.indexInBuildingManagerList].price;

            if (PlayerData.GetMoney() < price)
            {
                return;
            }
            
            BuildingManager.Instance.SetObject();
            if (BuildingManager.Instance.objectForBuild == null)
            {
                PlayerData.SpendMoney(price);
                objectData.isNew = false;

                if (objectData.indexInBuildingManagerList == 1)
                {
                    LevelManager.Instance.AddCountLockers(5);
                }
                
                panelObject.SetActive(false);
            }
            return;
        }
        
        BuildingManager.Instance.SetObject();
        if (BuildingManager.Instance.objectForBuild == null)
        {
            panelObject.SetActive(false);
        }
    }

    public void BtSellPressed()
    {
        ObjectData objectData = currentGameObjectForPanel.GetComponentInChildren<ObjectData>();
        PlayerData.AddMoney(objectData.price);
        
        if (objectData.indexInBuildingManagerList == 1)
        {
            LevelManager.Instance.TakeAwayCountLockers(5);
        }
        
        Destroy(currentGameObjectForPanel);
        Close();
    }

    public void BtActivateMove()
    {
        btRotate.SetActive(true);
        btSet.SetActive(true);
        btActivateMove.SetActive(false);
        btSell.SetActive(false);
        
        currentGameObjectForPanel.GetComponentInChildren<ObjectSettings>().ActivateMoveObject();
        
        ObjectData objectData = currentGameObjectForPanel.GetComponentInChildren<ObjectData>();
        
        objectData.positionBeforeMove = currentGameObjectForPanel.transform.position;
        objectData.rotationBeforeMove = currentGameObjectForPanel.transform.rotation;
    }
}
