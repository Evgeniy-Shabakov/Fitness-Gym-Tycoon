using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIManagerMain : MonoBehaviour
{
    public static UIManagerMain Instance;
    
    [SerializeField] private TextMeshProUGUI textForMoney;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void SetTextMoney(int money)
    {
        textForMoney.text = "" + money;
    }

    public void CloseAllPanels()
    {
        if (UIManagerPanelObject.Instance.panelObject.activeSelf)
        {
            UIManagerPanelObject.Instance.Close();
        }
        
        if (UIManagerPanelHumanClient.Instance.panelHumanClient.activeSelf)
        {
            UIManagerPanelHumanClient.Instance.Close();
        }

        if (UIManagerPanelGameShop.Instance.panelGameShop.activeSelf)
        {
            UIManagerPanelGameShop.Instance.Close();
        }
    }
    
    public bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
