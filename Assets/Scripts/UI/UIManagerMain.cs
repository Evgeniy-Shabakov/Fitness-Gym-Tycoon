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
        
        [SerializeField] private TextMeshProUGUI textMassage;
        private readonly List<string> _messages = new List<string>();
        
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
            textForMoney.text = money.ToString("N0");
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

        public void AddNewMessage(string s)
        {
            _messages.Add(s);
            Invoke(nameof(DeleteFirstMessage), 5f);
            SetTextMessages();
        }

        private void DeleteFirstMessage()
        {
            _messages.RemoveAt(0);
            SetTextMessages();
        }

        private void SetTextMessages()
        {
            var s = "";
            foreach (var m in _messages)
            {
                s += m + "\n";
            }
            
            textMassage.text = s;
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
            
            if (UIManagerPanelAccounting.Instance.panel.activeSelf)
            {
                UIManagerPanelAccounting.Instance.Close();
            }
            
            if (UIManagerPanelHireWorkers.Instance.panel.activeSelf)
            {
                UIManagerPanelHireWorkers.Instance.Close();
            }
        }
    
        public bool IsPointerOverUIObject() 
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            for (var index = 0; index < results.Count; index++)
            {
                var r = results[index];
                if (r.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
                {
                    results.Remove(r);
                }
            }

            return results.Count > 0;
        }
    }
}
