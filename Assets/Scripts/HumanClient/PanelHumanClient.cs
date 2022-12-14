using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHumanClient : MonoBehaviour
{
    private Camera mainCamera;
    
    private GameObject canvas;
    private GameObject panelGridLoyautGroup;
    
    private HumanControls humanControls;

    [SerializeField] private GameObject prefabImageTargetPanelHumanClient;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        humanControls = GetComponent<HumanControls>();
    }

    private void OnMouseUpAsButton()
    {
        CameraController.Instance.objectForFollow = gameObject;
        
        GameObject panelHumanClient = canvas.transform.Find("PanelHumanClient").gameObject;
        
        if (panelHumanClient.activeSelf) UIManager.Instance.ClosePanelHumanClient();
        
        panelHumanClient.SetActive(true);

        panelGridLoyautGroup = panelHumanClient.transform.GetChild(1).gameObject;

        for (int i = 0; i < humanControls.countTargets; i++)
        {
            GameObject image = Instantiate(prefabImageTargetPanelHumanClient, panelGridLoyautGroup.transform);
            
            int j = humanControls.targetsIndexes[i];
            image.GetComponent<Image>().sprite = BuildingManager.Instance.objectsForBuilding[j].sprite;
        }
    }
}
