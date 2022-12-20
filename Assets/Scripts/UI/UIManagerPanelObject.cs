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
    
    private GameObject currentGameObjectForBuildPanel;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void Close()
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
        panelObject.SetActive(false);
    }
    
    public void Open(GameObject current)
    {
        UIManagerMain.Instance.CloseAllPanels();
        
        currentGameObjectForBuildPanel = current;
            
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
        textPriceObject.text = BuildingManager.Instance.objectsForBuilding[i].price + "";
        textDescriptionObject.text = BuildingManager.Instance.objectsForBuilding[i].description;
    }

    public void BtSetPressed()
    {
        BuildingManager.Instance.SetObject();
        if (BuildingManager.Instance.objectForBuild == null)
        {
            panelObject.SetActive(false);
        }
    }

    public void BtSellPressed()
    {
        Destroy(currentGameObjectForBuildPanel);
        Close();
    }

    public void BtActivateMove()
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
}
