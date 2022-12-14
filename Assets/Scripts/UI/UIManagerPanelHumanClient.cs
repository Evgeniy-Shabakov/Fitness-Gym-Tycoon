using BuildingSystem;
using HumanClient;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerPanelHumanClient : MonoBehaviour
{
    public static UIManagerPanelHumanClient Instance;
    
    public GameObject panelHumanClient;
    
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

    public void Close()
    {
        GameObject humanClosedPanel = currentGameObjectForPanelHumanClient;
        currentGameObjectForPanelHumanClient = null;
        
        foreach(Transform child in panelHumanClientTargets.transform)
        {
            Destroy(child.gameObject);
        }
        
        panelHumanClient.SetActive(false);
        humanClosedPanel.GetComponent<HumanReactionControl>().ClearHumanReactionSprite();
        if (humanClosedPanel.GetComponent<HumanClientData>().trainingIsFinished)
        {
            humanClosedPanel.GetComponent<HumanReactionControl>().SetSmileAboveHuman();
        }
    }

    public void OpenAndFill(GameObject currentClient)
    {
        UIManagerMain.Instance.CloseAllPanels();
        
        panelHumanClient.SetActive(true);
        
        currentGameObjectForPanelHumanClient = currentClient;
        CameraController.Instance.objectForFollow = currentGameObjectForPanelHumanClient;
        currentGameObjectForPanelHumanClient.GetComponent<HumanReactionControl>().SetCrystalAboveHuman();
        
        var humanClientData = currentGameObjectForPanelHumanClient.GetComponent<HumanClientData>();
        
        for (int i = 0; i < LevelManager.NumberTargetsHumanClient; i++)
        {
            GameObject image = Instantiate(prefabImageTargetPanelHumanClient, panelHumanClientTargets.transform);
            
            ObjectType type = humanClientData.targetsArray[i];
            image.transform.GetChild(0).GetComponent<Image>().sprite = BuildingManager.Instance.FindObject(type).sprite;
        }

        Invoke("UpdateData", Time.deltaTime);
    }

    public void UpdateData()
    {
        var humanClientData = currentGameObjectForPanelHumanClient.GetComponent<HumanClientData>();
            
        for (int i = 0; i < LevelManager.NumberTargetsHumanClient; i++)
        {   
            GameObject image = panelHumanClientTargets.transform.GetChild(i).gameObject;
            
            if (i < humanClientData.indexInTargetsArray)
            {   
                if (humanClientData.targetsStatus[i] == true)
                {
                    image.transform.GetChild(1).GetComponent<Image>().sprite = spriteStatusTrue;
                }

                else
                {
                    image.transform.GetChild(1).GetComponent<Image>().sprite = spriteStatusFalse;
                }
            }
        }
        
        sliderMood.value = humanClientData.GetMood();
        
        if (sliderMood.value <= LevelManager.MoodSad)
        {
            imageSmile.sprite = spriteSmileSad;
            fillSliderMood.color = colorSad;
        }
        else if (sliderMood.value > LevelManager.MoodSad && sliderMood.value < LevelManager.MoodHappy)
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

        Close();
        currentGameObjectForPanelHumanClient = clientSpawner.transform.GetChild(i).gameObject;
        OpenAndFill(currentGameObjectForPanelHumanClient);
    }
    
    public void BtPreviousClientPressed()
    {
        int i = currentGameObjectForPanelHumanClient.transform.GetSiblingIndex();

        if (i == 0) i = clientSpawner.transform.childCount - 1;
        else i--;
        
        Close();
        currentGameObjectForPanelHumanClient = clientSpawner.transform.GetChild(i).gameObject;
        OpenAndFill(currentGameObjectForPanelHumanClient);
    }
}
