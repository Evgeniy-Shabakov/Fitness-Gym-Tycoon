using UnityEngine;
using TMPro;

namespace UI
{
    public class UIManagerPanelAccounting : MonoBehaviour
    {
        public static UIManagerPanelAccounting Instance;

        public GameObject panel;
        
        [SerializeField] private TextMeshProUGUI textTotalVisit;
        [SerializeField] private TextMeshProUGUI textTotalMonth;
        [SerializeField] private TextMeshProUGUI textTotalSixMonth;
        [SerializeField] private TextMeshProUGUI textTotalYear;
        [SerializeField] private TextMeshProUGUI textSaleEquipment;
        [SerializeField] private TextMeshProUGUI textRent;
        [SerializeField] private TextMeshProUGUI textPurchaseEquipment;
        [SerializeField] private TextMeshProUGUI textTax;
        [SerializeField] private TextMeshProUGUI textTotal;
        
        private void Awake()
        {
            Instance = this;
        }

        public void Open()
        {
            panel.SetActive(true);
        }

        public void Close()
        {
            panel.SetActive(false);
        }

        public void SetTextTotalVisit(int value)
        {
            textTotalVisit.text = value + "";
        }
        public void SetTextTotalMonth(int value)
        {
            textTotalMonth.text = value + "";
        }
        
        public void SetTextTotalSixMonth(int value)
        {
            textTotalSixMonth.text = value + "";
        }
        
        public void SetTextTotalYear(int value)
        {
            textTotalYear.text = value + "";
        }
        
        public void SetTextSaleEquipment(int value)
        {
            textSaleEquipment.text = value + "";
        }
        
        public void SetTextRent(int value)
        {
            textRent.text = value + "";
        }
        
        public void SetTextPurchaseEquipment(int value)
        {
            textPurchaseEquipment.text = value + "";
        }
        
        public void SetTextTax(int value)
        {
            textTax.text = value + "";
        }
        
        public void SetTextTotal(int value)
        {
            textTotal.text = value + "";
        }
    }
}
