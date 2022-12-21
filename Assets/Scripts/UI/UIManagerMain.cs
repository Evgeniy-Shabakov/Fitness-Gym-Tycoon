using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManagerMain : MonoBehaviour
{
    public static UIManagerMain Instance;
    
    [SerializeField] private TextMeshProUGUI textForMoney;
    
    [SerializeField] private Slider sliderRating;
    [SerializeField] private Image fillSliderRating;
    [SerializeField] private Color colorHappy;
    [SerializeField] private Color colorMiddle;
    [SerializeField] private Color colorSad;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void SetTextMoney(int money)
    {
        textForMoney.text = "" + money;
    }

    public void SetRating(int rating)
    {
        sliderRating.value = rating;
        
        if (sliderRating.value <= LevelManager.moodSad)
        {
            fillSliderRating.color = colorSad;
        }
        else if (sliderRating.value > LevelManager.moodSad && sliderRating.value < LevelManager.moodHappy)
        {
            fillSliderRating.color = colorMiddle;
        }
        else
        {
            fillSliderRating.color = colorHappy;
        }
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