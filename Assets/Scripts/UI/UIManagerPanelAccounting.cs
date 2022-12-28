using UnityEngine;

namespace UI
{
    public class UIManagerPanelAccounting : MonoBehaviour
    {
        public static UIManagerPanelAccounting Instance;

        public GameObject panel;
        
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
