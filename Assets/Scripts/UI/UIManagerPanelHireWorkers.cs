using System;
using UnityEngine;
using Worker;

namespace UI
{
    public class UIManagerPanelHireWorkers : MonoBehaviour
    {
        public static UIManagerPanelHireWorkers Instance;
        
        public GameObject panel;

        [SerializeField] private GameObject workersContainer;
        [SerializeField] private GameObject prefabReceptionist;
        [SerializeField] private GameObject prefabJanitor;
        
        private void Awake()
        {
            Instance = this;
        }
        
        public void Close()
        {
            panel.SetActive(false);
        }

        public void Open()
        {
            panel.SetActive(true);
        }

        public void AddReceptionist()
        {
            Instantiate(prefabReceptionist, workersContainer.transform);
        }

        public void RemoveReceptionist()
        {
            foreach (Transform child in workersContainer.transform)
            {
                if (child.CompareTag("Receptionist"))
                {
                    Destroy(child.gameObject);
                    return;
                }
            }
        }
        
        public void AddJanitor()
        {
            Instantiate(prefabJanitor, workersContainer.transform);
        }

        public void RemoveJanitor()
        {
            foreach (Transform child in workersContainer.transform)
            {
                if (child.CompareTag("Janitor"))
                {
                    Destroy(child.gameObject);
                    return;
                }
            }
        }
    }
}
