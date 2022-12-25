using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIManagerMain : MonoBehaviour
    {
        public static UIManagerMain Instance;
    
        [SerializeField] private TextMeshProUGUI textForMoney;
        
        [SerializeField] private TextMeshProUGUI textMenLockers;
        [SerializeField] private TextMeshProUGUI textNumberMen;
        [SerializeField] private TextMeshProUGUI textWomenLockers;
        [SerializeField] private TextMeshProUGUI textNumberWomen;
        
        [SerializeField] private TextMeshProUGUI textDay;
        [SerializeField] private TextMeshProUGUI textMonth;
        [SerializeField] private TextMeshProUGUI textYear;
    
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

        public void SetTextMenLockers(int lockers)
        {
            textMenLockers.text = "" + lockers;
        }
        
        public void SetTextWomenLockers(int lockers)
        {
            textWomenLockers.text = "" + lockers;
        }

        public void SetTextNumberMen(int men)
        {
            textNumberMen.text = "" + men;
        }

        public void SetTextNumberWomen(int women)
        {
            textNumberWomen.text = "" + women;
        }

        public void SetTextDateSimulation(int day, int month, int year)
        {
            textDay.text = day + " d";
            textMonth.text = month + " m";
            textYear.text = year + " y";
        }

        public void SetRating(int rating)
        {
            sliderRating.value = rating;
        
            if (sliderRating.value <= LevelManager.MoodSad)
            {
                fillSliderRating.color = colorSad;
            }
            else if (sliderRating.value > LevelManager.MoodSad && sliderRating.value < LevelManager.MoodHappy)
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
        
            if (UIManagerPanelPricePolicy.Instance.panel.activeSelf)
            {
                UIManagerPanelPricePolicy.Instance.Close();
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
}
