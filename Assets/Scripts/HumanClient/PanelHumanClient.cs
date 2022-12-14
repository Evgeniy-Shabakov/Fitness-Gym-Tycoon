using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelHumanClient : MonoBehaviour
{
    private static GameObject currentGameObjectForPanel;
    
    private Camera mainCamera;
    
    private GameObject canvas;
    private GameObject panelHumanClient;
    private GameObject panelHumanClientGridLoyautGroup;
    
    private HumanControls humanControls;

    [SerializeField] private GameObject prefabImageTargetPanelHumanClient;
    [SerializeField] private Sprite spriteStatusTrue;
    [SerializeField] private Sprite spriteStatusFalse;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        panelHumanClient = canvas.transform.Find("PanelHumanClient").gameObject;
        
        humanControls = GetComponent<HumanControls>();
        humanControls.IndexInTargetsArrayChanged.AddListener(SetNewStatus);
    }

    private void OnMouseUpAsButton()
    {
        CameraController.Instance.objectForFollow = gameObject;
        currentGameObjectForPanel = gameObject;
        
        if (panelHumanClient.activeSelf) UIManager.Instance.ClosePanelHumanClient();
        
        panelHumanClient.SetActive(true);

        panelHumanClientGridLoyautGroup = panelHumanClient.transform.GetChild(2).gameObject;

        for (int i = 0; i < humanControls.countTargets; i++)
        {
            GameObject image = Instantiate(prefabImageTargetPanelHumanClient, panelHumanClientGridLoyautGroup.transform);
            
            int j = humanControls.targetsArray[i];
            image.transform.GetChild(0).GetComponent<Image>().sprite = BuildingManager.Instance.objectsForBuilding[j].sprite;

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
    }

    private void SetNewStatus()
    {
        if (gameObject != currentGameObjectForPanel) return;

        int previousIndex = humanControls.indexInTargetsArray - 1;
        Image imageForStatus = panelHumanClientGridLoyautGroup.transform.GetChild(previousIndex).transform.GetChild(1).GetComponent<Image>();
        
        if (humanControls.targetsStatus[previousIndex])
        {
            imageForStatus.sprite = spriteStatusTrue;
        }

        else
        {
            imageForStatus.sprite = spriteStatusFalse;
        }
    }
}
