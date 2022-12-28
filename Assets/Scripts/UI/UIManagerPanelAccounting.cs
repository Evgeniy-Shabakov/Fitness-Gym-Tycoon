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
    }
}
